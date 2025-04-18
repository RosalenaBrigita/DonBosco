using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DonBosco.API;
using UnityEngine;
using Inventory;
using Inventory.Model;
using UnityEngine.Networking;

//#if UNITY_EDITOR
using UnityEngine.SceneManagement;
//#endif

namespace DonBosco.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager instance;
        public static SaveManager Instance { get { return instance; } }

        [SerializeField] LoadScene loadData;

        private List<ISaveLoad> listeners = new List<ISaveLoad>();
        private SaveData saveData;
        const string FILENAME = "/playerdata.dat";
        public string currentAccountID;

        public bool HasSaveData { get; private set; }
        private bool isDispatching = false;

        private string lastLoadedScene = "";
        public bool isLoggedIn = false;  // True kalau user login
        private string serverURL = "http://localhost/DonBosco/save_game.php"; 

        private void Awake()
        {
            instance = this;

            //Try to load the game in the beginning
            HasSaveData = ReadSaveDataLocal();

#if UNITY_EDITOR
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == "GAMEPLAY")
                {
                    loadData.ExecuteLoadScene();
                    return;
                }
            }
#endif
            loadData.ExecuteLoadScene(() => loadData.AddToLoad());
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            lastLoadedScene = scene.name; // Update dengan scene terakhir yang dimuat
            //Debug.Log($"Scene terakhir yang dimuat: {lastLoadedScene}");
        }

        // Menyimpan status objek interaktif
        public void SetObjectDisabled(string objectID, bool isDisabled)
        {
            if (string.IsNullOrEmpty(objectID))
            {
                Debug.LogWarning($"SetObjectDisabled gagal: objectID kosong.");
                return;
            }

            if (saveData == null)
                saveData = new SaveData();

            if (saveData.objectStates == null)
                saveData.objectStates = new List<ObjectStateData>();

            // Cek apakah objectID sudah ada dalam list
            ObjectStateData existingObject = saveData.objectStates.Find(obj => obj.objectID == objectID);

            if (existingObject != null)
            {
                existingObject.isDisabled = isDisabled;
            }
            else
            {
                saveData.objectStates.Add(new ObjectStateData(objectID, isDisabled));
            }
        }


        public bool IsObjectDisabled(string objectID)
        {
            if (string.IsNullOrEmpty(objectID))
                return false;

            if (saveData == null || saveData.objectStates == null)
                return false;

            ObjectStateData objectState = saveData.objectStates.Find(obj => obj.objectID == objectID);
            return objectState != null && objectState.isDisabled;
        }

        public async Task SaveGame()
        {
            // Pastikan saveData tidak null
            if (saveData == null)
            {
                saveData = new SaveData();
                Debug.LogWarning("saveData masih null, membuat instance baru.");
            }

            for (int i = 0; i < listeners.Count; i++)
            {
                await listeners[i].Save(saveData);
            }

            // Cek semua scene yang sedang aktif
            string sceneToSave = lastLoadedScene; // Ambil nama scene yang sedang dimainkan

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.name == "SewingMinigame")
                {
                    sceneToSave = "MAP_RUMAHKARMANtes"; // Jika sedang di SewingMinigame, paksa save ke Rumah Karman
                    break;
                }
            }

            saveData.currentScene = sceneToSave;

            if (InventoryController.Instance != null)
            {
                saveData.inventoryItems = new List<InventoryItem>(InventoryController.Instance.GetInventorySO().GetCurrentInventoryState().Values);
            }

            if (saveData.objectStates == null)
                saveData.objectStates = new List<ObjectStateData>();

            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                if (!obj.activeSelf) // Jika objek tidak aktif, simpan statusnya
                {
                    SetObjectDisabled(obj.name, true);
                }
            }

            Debug.Log($"[SaveGame] Saving {saveData.objectStates.Count} disabled objects.");

            string json = saveData.ToJson();

            if (isLoggedIn && !string.IsNullOrEmpty(currentAccountID))
            {
                Debug.Log("Attempting cloud save...");
                bool cloudSuccess = await SaveToCloud(json);

                if (cloudSuccess)
                {
                    string cloudData = await LoadFromCloud();
                    WriteSaveDataLocal(cloudData);
                    Debug.Log("Cloud save successful");
                }
                else
                {
                    Debug.LogWarning("Cloud save failed, saving locally");
                    WriteSaveDataLocal(json);
                }
            }
            else
            {
                Debug.Log("Guest mode, saving locally");
                WriteSaveDataLocal(json);
            }

            await Task.CompletedTask;
        }

        private async Task<bool> SaveToCloud(string jsonData)
        {
            try
            {
                // Pastikan jsonData valid
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.LogError("Invalid jsonData");
                    return false;
                }

                // Buat payload manual untuk menghindari masalah serialisasi
                string jsonPayload = $"{{\"user_id\":\"{currentAccountID}\",\"save_data\":{jsonData}}}";

                Debug.Log("Sending JSON payload: " + jsonPayload);

                using (UnityWebRequest request = new UnityWebRequest(serverURL, "POST"))
                {
                    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");
                    request.timeout = 10;

                    var operation = request.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();

                    string responseText = request.downloadHandler.text;
                    Debug.Log("Server response: " + responseText);

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Request error: {request.error}");
                        return false;
                    }

                    try
                    {
                        var response = JsonUtility.FromJson<CloudResponse>(responseText);
                        return response.status == "success";
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Response parsing error: {e.Message}");
                        return false;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"SaveToCloud exception: {e}");
                return false;
            }
        }

        [System.Serializable]
        private class CloudResponse
        {
            public string status;
            public string message;
        }

        [System.Serializable]
        private class CloudLoadResponse
        {
            public string status;
            public SaveData save_data;
            public string message;
            public int user_id;
        }
        /// <summary>
        /// Load the game from the save file
        /// </summary>
        /// <returns>True if the save file exists</returns>
        public async Task<bool> LoadGame()
        {
            try
            {
                // Validasi awal
                if (isLoggedIn && string.IsNullOrEmpty(currentAccountID))
                {
                    Debug.LogError("Cannot load - logged in but no account ID set");
                    HasSaveData = false;
                    return false;
                }

                bool loadSuccess = false;

                // Untuk user yang login
                if (HasSaveData && isLoggedIn && !string.IsNullOrEmpty(currentAccountID))
                {
                    Debug.Log("Attempting cloud load...");
                    string cloudData = await LoadFromCloud();

                    if (!string.IsNullOrEmpty(cloudData))
                    {
                        saveData = JsonUtility.FromJson<SaveData>(cloudData);

                        if (saveData == null || string.IsNullOrEmpty(saveData.currentScene))
                        {
                            Debug.LogError("Invalid save data from cloud");
                            HasSaveData = false;
                            return false;
                        }
                        Debug.Log("Cloud load successful!");
                        loadSuccess = true;
                    }
                    else // Fallback ke local save
                    {
                        Debug.Log("Falling back to local account save...");
                        loadSuccess = ReadSaveDataAccount(currentAccountID);
                    }
                }
                else // Untuk guest
                {
                    Debug.Log("Loading local guest save...");
                    loadSuccess = ReadSaveDataLocal();
                }

                // Update status HasSaveData
                HasSaveData = loadSuccess;

                if (!loadSuccess)
                {
                    Debug.Log("No valid save data found");
                    return false;
                }

                // Proses data yang telah dimuat
                Debug.Log($"Processing save data for scene: {saveData.currentScene}");

                // Memuat data ke semua sistem yang terdaftar
                for (int i = 0; i < listeners.Count; i++)
                {
                    try
                    {
                        await listeners[i].Load(saveData);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error loading {listeners[i].GetType()}: {e}");
                    }
                }

                // Handle inventory
                if (saveData.inventoryItems != null && InventoryController.Instance != null)
                {
                    try
                    {
                        InventoryController.Instance.GetInventorySO().Initialize();
                        InventoryController.Instance.inventoryUI.InitializeInventoryUI(
                            InventoryController.Instance.GetInventorySO().Size);
                        await Task.Delay(100); // Beri waktu untuk UI
                        InventoryController.Instance.GetInventorySO().LoadInventory(saveData.inventoryItems);
                        Debug.Log($"Loaded {saveData.inventoryItems.Count} inventory items");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Inventory load error: {e}");
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"LoadGame error: {e}");
                HasSaveData = false;
                return false;
            }
        }

        private async Task<string> LoadFromCloud()
        {
            if (string.IsNullOrEmpty(currentAccountID) || currentAccountID == "0")
            {
                Debug.LogError("Invalid currentAccountID for cloud load");
                return null;
            }

            try
            {
                WWWForm form = new WWWForm();
                form.AddField("user_id", currentAccountID);

                using (UnityWebRequest www = UnityWebRequest.Post(serverURL.Replace("save_game.php", "load_game.php"), form))
                {
                    www.timeout = 10;
                    var operation = www.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Cloud load failed: {www.error}");
                        return null;
                    }

                    string jsonResponse = www.downloadHandler.text;
                    Debug.Log($"Raw cloud response: {jsonResponse}");

                    // Langsung deserialize ke CloudLoadResponse
                    var response = JsonUtility.FromJson<CloudLoadResponse>(jsonResponse);

                    if (response == null || response.status != "success")
                    {
                        Debug.LogError($"Invalid cloud response: {response?.message}");
                        return null;
                    }

                    if (response.user_id.ToString() != currentAccountID)
                    {
                        Debug.LogError($"Account ID mismatch! Expected {currentAccountID}, got {response.user_id}");
                        return null;
                    }

                    // Kembalikan JSON string dari save_data
                    return JsonUtility.ToJson(response.save_data);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"LoadFromCloud exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Delete the save data from the local file when new game is started
        /// </summary>
        public void DeleteSaveData()
        {
            string path = "";

            if (isLoggedIn)
            {
                // Jika pemain sudah login, hapus data berdasarkan ID akun
                if (!string.IsNullOrEmpty(currentAccountID) && int.TryParse(currentAccountID, out int userId))
                {
                    DeleteCloudSave();
                    path = Application.persistentDataPath + "/" + currentAccountID + ".dat";
                }
                else
                {
                    Debug.LogError("Current Account ID is null, empty, or not a valid number.");
                    return;
                }

            }
            else
            {
                // Jika pemain belum login, hapus data dari file default
                path = Application.persistentDataPath + "/" + FILENAME;
            }

            // Hapus file jika ada
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                HasSaveData = false;
                saveData = null;
            }
            else
            {
                Debug.LogError("Filesave to delete does not exist: " + path);
            }
        }

        public async Task<bool> DeleteCloudSave()
        {
            if (string.IsNullOrEmpty(currentAccountID))
            {
                Debug.LogWarning("Cannot delete: currentAccountID is null or empty");
                return false;
            }

            using (UnityWebRequest www = UnityWebRequest.Post(serverURL.Replace("save_game.php", "delete_game.php"), ""))
            {
                WWWForm form = new WWWForm();
                form.AddField("user_id", currentAccountID);

                www.uploadHandler = new UploadHandlerRaw(form.data);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.timeout = 10;

                var operation = www.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Delete cloud save failed: {www.error}");
                    return false;
                }

                Debug.Log("Cloud save deleted successfully.");
                return true;
            }
        }


        /// <summary>
        /// Write the save data to the local file
        /// </summary>
        /// <param name="json"></param>
        private void WriteSaveDataLocal(string json)
        {
            string path;
            if (string.IsNullOrEmpty(currentAccountID))
            {
                path = Application.persistentDataPath + FILENAME;
            }
            else
            {
                path = Application.persistentDataPath + "/" + currentAccountID + ".dat";
            }
            Debug.Log($"<color=green>[Save System] Menyimpan data ke:</color> <b>{path}</b>");
            try
            {
                // Gunakan StreamWriter untuk menulis file dengan encoding UTF-8 tanpa BOM
                using (var writer = new System.IO.StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
                {
                    writer.Write(json);
                }

                HasSaveData = true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error saat menulis save file: {e}");
            }
        }

        /// <summary>
        /// Read the save data from the local file, this is used when the player is not logged in
        /// </summary>
        /// <returns></returns>
        public bool ReadSaveDataLocal()
        {
            saveData = null;
            string path = Application.persistentDataPath + FILENAME;

            if (System.IO.File.Exists(path))
            {
                try
                {
                    // Gunakan StreamReader untuk membaca file dengan encoding UTF-8 tanpa BOM
                    using (var reader = new System.IO.StreamReader(path, new System.Text.UTF8Encoding(false)))
                    {
                        string jsonState = reader.ReadToEnd();

                        if (string.IsNullOrEmpty(jsonState))
                        {
                            Debug.LogError("Save file is empty.");
                            return false; // Kembalikan false jika file kosong
                        }

                        saveData = JsonUtility.FromJson<SaveData>(jsonState);
                        HasSaveData = true;

                        if (saveData.objectStates == null)
                            saveData.objectStates = new List<ObjectStateData>();

                        Debug.Log($"Loaded SaveData: {saveData.objectStates.Count} objects disabled.");

                        foreach (var obj in saveData.objectStates)
                        {
                            Debug.Log($"Loaded object: {obj.objectID}, isDisabled: {obj.isDisabled}");
                        }

                        GameEventsManager.Instance.miscEvents.ChangeData(saveData);
                        return true;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error saat membaca save file: {e}");
                }
            }

            HasSaveData = false;
            return false;
        }


        /// <summary>
        /// Read the save data from the local file based on the account id, this is used when the player is logged in
        /// </summary>
        /// <param name="id"></param>
        public bool ReadSaveDataAccount(string id)
        {
            saveData = null;
            string path = Application.persistentDataPath + "/" + id + ".dat";

            if (System.IO.File.Exists(path))
            {
                try
                {
                    string json = System.IO.File.ReadAllText(path);
                    Debug.Log($"Loading from {path}\nData: {json}");

                    saveData = JsonUtility.FromJson<SaveData>(json);

                    if (saveData == null)
                    {
                        Debug.LogError("Failed to parse save data");
                        return false;
                    }

                    // Inisialisasi list yang mungkin null
                    if (saveData.objectStates == null)
                        saveData.objectStates = new List<ObjectStateData>();
                    if (saveData.inventoryItems == null)
                        saveData.inventoryItems = new List<InventoryItem>();

                    HasSaveData = true;
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error reading account save: {e}");
                    // Coba hapus file corrupt
                    try { System.IO.File.Delete(path); } catch { }
                }
            }

            HasSaveData = false;
            return false;
        }

        #region Subscription
        public void Subscribe(ISaveLoad saveLoad)
        {
            if (isDispatching)
            {
                Debug.LogError($"{saveLoad} is trying to subscribe while dispatching");
                return;
            }
            listeners.Add(saveLoad);
        }

        public void Unsubscribe(ISaveLoad saveLoad)
        {
            if (isDispatching)
            {
                Debug.LogError($"{saveLoad} is trying to unsubscribe while dispatching");
                return;
            }
            listeners.Remove(saveLoad);
        }
        #endregion
    }
}

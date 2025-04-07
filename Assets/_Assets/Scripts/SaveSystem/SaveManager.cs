using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DonBosco.API;
using UnityEngine;
using Inventory;
using Inventory.Model;

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

        public string lastLoadedScene = "";

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
            Debug.Log($"Scene terakhir yang dimuat: {lastLoadedScene}");
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
            WriteSaveDataLocal(json);
            await Task.CompletedTask;
        }


        /// <summary>
        /// Load the game from the save file
        /// </summary>
        /// <returns>True if the save file exists</returns>
        public async Task<bool> LoadGame()
        {
            if (saveData == null)
            {
                saveData = new SaveData();
            }
            try
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    await listeners[i].Load(saveData);
                }

                if (saveData.inventoryItems != null && InventoryController.Instance != null)
                {
                    InventoryController.Instance.GetInventorySO().Initialize();
                    InventoryController.Instance.inventoryUI.InitializeInventoryUI(InventoryController.Instance.GetInventorySO().Size);

                    await Task.Delay(100); // Beri waktu UI untuk refresh

                    InventoryController.Instance.GetInventorySO().LoadInventory(saveData.inventoryItems);
                    Debug.Log($"Inventory Loaded: {saveData.inventoryItems.Count} items");
                }

                return true;
            }
            catch (System.ArgumentException e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        /// <summary>
        /// Delete the save data from the local file when new game is started
        /// </summary>
        public void DeleteSaveData()
        {
            string path;
            if (string.IsNullOrEmpty(currentAccountID))
            {
                // If the player is not logged in, delete the data from the default file
                path = Application.persistentDataPath + FILENAME;
            }
            else
            {
                // If the player is logged in, delete the data from the file based on the account id
                path = Application.persistentDataPath + "/" + currentAccountID + ".dat";
            }

            // Then delete the file
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

            string fileName = "/" + id + ".dat";
            string path = Application.persistentDataPath + fileName;
            if (System.IO.File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    System.IO.FileStream file = System.IO.File.Open(path, System.IO.FileMode.Open);
                    string jsonState = (string)formatter.Deserialize(file);
                    file.Close();
                    saveData = JsonUtility.FromJson<SaveData>(jsonState);
                    HasSaveData = true;
                    GameEventsManager.Instance.miscEvents.ChangeData(saveData);
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
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

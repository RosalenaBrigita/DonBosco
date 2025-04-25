using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using DonBosco.SaveSystem;
using System.Threading.Tasks;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text alertText;
    public TMP_Text statusText; 
    public TMP_Text usernameText; 

    // UI GameObjects to control
    public GameObject loginModal;
    public GameObject loginButton;
    public GameObject logoutButton;
    public GameObject statusModal; 

    // For status modal interaction
    public Image statusBG; 

    void Start()
    {
        string savedToken = PlayerPrefs.GetString("token", "");
        if (!string.IsNullOrEmpty(savedToken))
        {
            StartCoroutine(AutoLogin(savedToken));
        }
    }

    public void OnLoginButtonClicked()
    {
        // Clear previous alerts
        alertText.text = "";

        // Show loading/status modal
        statusModal.SetActive(true);
        statusBG.raycastTarget = true;
        statusText.text = "Connecting to server...";

        StartCoroutine(LoginRequest(usernameInput.text, passwordInput.text));
    }

    public void OnLogoutButtonClicked()
    {
        PlayerPrefs.DeleteKey("token");
        PlayerPrefs.DeleteKey("username");

        statusModal.SetActive(true);
        statusBG.raycastTarget = true;
        loginButton.SetActive(true);
        logoutButton.SetActive(false);
        usernameText.text = "";

        statusText.text = "Logged out successfully.";
        SaveManager.Instance.isLoggedIn = false;
        SaveManager.Instance.currentAccountID = null;
        // Start game data loading
        UpdateUIAfterLogin();
    }

    IEnumerator LoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/DonBosco/login.php", form);

        yield return www.SendWebRequest();

        // First check network errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            alertText.text = "Connection error: " + www.error;
            alertText.color = Color.red;
            statusText.text = "Connection failed";
            statusBG.raycastTarget = true;
            yield break; // Exit the coroutine
        }

        // Then handle the response
        string jsonResponse = www.downloadHandler.text;
        Debug.Log("Raw JSON: " + jsonResponse);

        LoginResponse response;
        try
        {
            response = JsonUtility.FromJson<LoginResponse>(jsonResponse);
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON Parse Error: " + e.Message);
            alertText.text = "Login error. Please try again.";
            alertText.color = Color.red;
            yield break;
        }

        // Now we can use yield since we're outside the try-catch
        if (response.status == "success")
        {
            SaveManager.Instance.isLoggedIn = true;
            SaveManager.Instance.currentAccountID = response.user_id.ToString();

            statusText.text = "Authentication successful!";
            yield return new WaitForSeconds(1f);

            usernameText.text = response.username;
            loginModal.SetActive(false);
            loginButton.SetActive(false);
            logoutButton.SetActive(true);

            alertText.text = "Welcome back, " + response.username + "!";
            alertText.color = Color.green;
            statusModal.SetActive(false);

            PlayerPrefs.SetString("username", response.username);
            PlayerPrefs.SetString("token", response.token);
            PlayerPrefs.SetInt("user_id", response.user_id);
            PlayerPrefs.Save();

            // Start loading game data after successful login
            UpdateUIAfterLogin();
            //yield return StartCoroutine(LoadAfterLogin());
        }
        else
        {
            alertText.text = response.message;
            alertText.color = Color.red;
            statusText.text = "Login failed";
            statusBG.raycastTarget = true;
        }
    }

    IEnumerator AutoLogin(string token)
    {
        statusModal.SetActive(true);
        statusText.text = "Checking saved session...";

        WWWForm form = new WWWForm();
        form.AddField("token", token);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/DonBosco/validate_token.php", form);
        yield return www.SendWebRequest(); // First yield outside try-catch

        // Debug the raw response
        string rawResponse = www.downloadHandler.text;
        Debug.Log("Raw validation response: " + rawResponse);

        if (www.result != UnityWebRequest.Result.Success)
        {
            alertText.text = "Connection error: " + www.error;
            alertText.color = Color.red;
            statusModal.SetActive(false);
            yield break;
        }

        LoginResponse response = null;
        try
        {
            response = JsonUtility.FromJson<LoginResponse>(rawResponse);
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON parse error: " + e.Message);
            alertText.text = "Session error. Please login again.";
            alertText.color = Color.red;
            statusModal.SetActive(false);
            yield break;
        }

        if (response == null)
        {
            alertText.text = "Invalid server response";
            alertText.color = Color.red;
            statusModal.SetActive(false);
            yield break;
        }

        if (response.status == "success")
        {
            // Validate user_id
            if (response.user_id <= 0)
            {
                alertText.text = "Invalid user account";
                alertText.color = Color.red;
                statusModal.SetActive(false);
                yield break;
            }

            SaveManager.Instance.isLoggedIn = true;
            SaveManager.Instance.currentAccountID = response.user_id.ToString();
            Debug.Log($"AutoLogin success! UserID: {response.user_id}");

            // Update UI and player prefs
            usernameText.text = response.username;
            loginModal.SetActive(false);
            loginButton.SetActive(false);
            logoutButton.SetActive(true);
            alertText.text = "Welcome back, " + response.username + "!";
            alertText.color = Color.green;
            statusModal.SetActive(false);

            // Start game data loading
            UpdateUIAfterLogin();
            //yield return StartCoroutine(LoadAfterLogin());
        }
        else
        {
            PlayerPrefs.DeleteKey("token");
            alertText.text = response.message ?? "Session expired";
            alertText.color = Color.red;
            statusModal.SetActive(false);
        }
    }

    public void QuitGame()
    {
        QuitPrevention quitHandler = FindObjectOfType<QuitPrevention>();
        if (quitHandler != null)
        {
            quitHandler.RequestImmediateQuit(); // Langsung quit
        }
        else
        {
            Application.Quit(); // Fallback
        }
    }

    private void UpdateUIAfterLogin()
    {
        // This will trigger the SaveManager to check for save data
        SaveManager.Instance.InitializeSaveSystem();
    }
        /*IEnumerator LoadAfterLogin()
        {
            // Create a task and wait for it to complete
            var loadTask = SaveManager.Instance.LoadGame();
            while (!loadTask.IsCompleted)
            {
                yield return null;
            }

            Debug.Log(loadTask.Result ? "Game loaded successfully" : "Failed to load game");
        }*/

        public void CloseStatusModal()
    {
        statusModal.SetActive(false);
        statusBG.raycastTarget = false;
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string status;
        public string message;
        public string token;
        public int user_id;
        public string username;
    }
}
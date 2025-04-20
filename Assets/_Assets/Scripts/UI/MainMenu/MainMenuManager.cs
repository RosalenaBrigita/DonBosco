using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace DonBosco
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager instance;
        public static MainMenuManager Instance => instance;

        [Header("References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject confirmationOverwriteSaveData;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] public UnityEvent onBackToMainMenu;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private void OnEnable()
        {
            // Subscribe to save data updates
            SaveSystem.SaveManager.OnSaveDataUpdated += UpdateMenuButtons;

            // Initial check (will be updated again when cloud save finishes checking)
            UpdateMenuButtons(SaveSystem.SaveManager.Instance.HasSaveData);
        }

        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            SaveSystem.SaveManager.OnSaveDataUpdated -= UpdateMenuButtons;
        }

        public void DeleteSave()
        {
            SaveSystem.SaveManager.Instance.DeleteSaveData();
            // No need to manually update buttons - event will handle it
        }

        public void StartNewGame()
        {
            if (SaveSystem.SaveManager.Instance.HasSaveData)
            {
                confirmationOverwriteSaveData.SetActive(true);
            }
            else
            {
                newGameButton.GetComponent<TransitionCaller>().TransitionFadeOut();
            }
        }

        public void PlayGame()
        {
            mainMenuCanvas.SetActive(false);
        }

        public void InitMainMenu()
        {
            mainMenuCanvas.SetActive(true);
            onBackToMainMenu?.Invoke();
        }

        // Renamed from CheckProgress for clarity
        private void UpdateMenuButtons(bool hasSaveData)
        {
            continueGameButton.interactable = hasSaveData;
            newGameButton.GetComponentInChildren<TMP_Text>().text = hasSaveData ? "New Game" : "Play";

            Debug.Log($"Menu updated - Save exists: {hasSaveData}");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
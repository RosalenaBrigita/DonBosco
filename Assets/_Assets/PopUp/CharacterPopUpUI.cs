using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DonBosco.SaveSystem;
using System.Threading.Tasks;

namespace DonBosco
{
    public class CharacterPopUpUI : MonoBehaviour, ISaveLoad
    {
        public static CharacterPopUpUI Instance { get; private set; }

        [Header("Main UI")]
        [SerializeField] private GameObject popupPanel;
        [SerializeField] private Image characterImage;
        [SerializeField] private TMP_Text characterNameText;
        [SerializeField] private TMP_Text characterDescriptionText;

        [Header("Real Photo UI")]
        [SerializeField] private GameObject realPhotoPanel;
        [SerializeField] private Image realPhotoImage;
        [SerializeField] private TMP_Text sourceCreditText;
        [SerializeField] private Button showRealPhotoButton;

        private HashSet<string> seenCharacterIDs = new HashSet<string>();
        private CharacterDataSO currentCharacterData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                SaveManager.Instance.Subscribe(this);
                DontDestroyOnLoad(gameObject);

                // Setup button callback
                showRealPhotoButton.onClick.AddListener(ToggleRealPhoto);
            }
            else
            {
                Destroy(gameObject);
            }
            popupPanel.SetActive(false);
            realPhotoPanel.SetActive(false);
        }

        public void ShowCharacterDetail(CharacterDataSO characterData)
        {
            if (characterData == null) return;

            currentCharacterData = characterData;
            InputManager.Instance.SetMovementActionMap(false);

            // Main display setup
            characterImage.sprite = characterData.characterImage;
            characterNameText.text = characterData.characterName;
            characterDescriptionText.text = characterData.description;

            // Real photo button visibility
            showRealPhotoButton.gameObject.SetActive(characterData.realImage != null);

            popupPanel.SetActive(true);
            seenCharacterIDs.Add(characterData.characterID);
        }

        private void ToggleRealPhoto()
        {
            bool showReal = !realPhotoPanel.activeSelf;

            if (showReal)
            {
                // Show real historical photo
                realPhotoImage.sprite = currentCharacterData.realImage;
                sourceCreditText.text = currentCharacterData.sourceCredit;
                showRealPhotoButton.GetComponentInChildren<TMP_Text>().text = "Tampilkan Gambar In-Game";

                // Hide cartoon image
                characterImage.gameObject.SetActive(false);
            }
            else
            {
                // Show cartoon image
                characterImage.gameObject.SetActive(true);
                showRealPhotoButton.GetComponentInChildren<TMP_Text>().text = "Tampilkan Foto Sejarah";
            }

            realPhotoPanel.SetActive(showReal);
        }

        public void HideCharacterDetail()
        {
            popupPanel.SetActive(false);
            realPhotoPanel.SetActive(false);
            InputManager.Instance.SetMovementActionMap(true);
        }

        #region ISaveLoad Implementation
        public async Task Load(SaveData saveData)
        {
            seenCharacterIDs.Clear();
            if (saveData?.seenCharacterIDs != null)
            {
                foreach (string id in saveData.seenCharacterIDs)
                {
                    seenCharacterIDs.Add(id);
                }
            }
            await Task.CompletedTask;
        }

        public async Task Save(SaveData saveData)
        {
            if (saveData != null)
            {
                saveData.seenCharacterIDs = new List<string>(seenCharacterIDs);
            }
            await Task.CompletedTask;
        }
        #endregion

        public bool HasSeenCharacter(string characterID)
        {
            return seenCharacterIDs.Contains(characterID);
        }
    }
}
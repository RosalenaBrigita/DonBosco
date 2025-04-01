using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPopUpUI : MonoBehaviour
{
    public static CharacterPopUpUI Instance { get; private set; } // Singleton opsional

    [Header("UI Elements")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterDescriptionText;

    private string currentCharacterID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Inisialisasi Singleton
        }
        else
        {
            Destroy(gameObject); // Pastikan tidak ada dua instance
        }

        popupPanel.SetActive(false); // Pastikan UI mati saat game dimulai
    }

    public void ShowCharacterDetail(CharacterDataSO characterData)
    {
        currentCharacterID = characterData.characterID;

        // Pastikan panel aktif sebelum mengubah elemen UI
        popupPanel.SetActive(true);

        characterImage.sprite = characterData.characterImage;
        characterNameText.text = characterData.characterName;
        characterDescriptionText.text = characterData.description;
    }

    public void HideCharacterDetail()
    {
        popupPanel.SetActive(false);

        // Simpan status hanya jika ID valid
        if (!string.IsNullOrEmpty(currentCharacterID))
        {
            PlayerPrefs.SetInt("CharacterSeen_" + currentCharacterID, 1);
            PlayerPrefs.Save();
        }

        currentCharacterID = null; // Reset ID agar tidak ada data sisa
    }
}

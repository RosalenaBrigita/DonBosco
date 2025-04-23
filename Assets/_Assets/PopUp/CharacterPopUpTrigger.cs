using UnityEngine;
using DonBosco;

public class CharacterPopUpTrigger : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;

    public void Show()
    {
        // Cek apakah karakter sudah pernah dilihat
        if (CharacterPopUpUI.Instance.HasSeenCharacter(characterData.characterID))
            return;

        CharacterPopUpUI.Instance.ShowCharacterDetail(characterData);
    }
}
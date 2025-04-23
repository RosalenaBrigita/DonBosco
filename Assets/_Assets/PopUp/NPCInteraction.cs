using UnityEngine;
using DonBosco;

public class NPCInteraction2D : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    [SerializeField] private Collider2D triggerCollider; // Collider yang digunakan untuk mendeteksi player

    private bool hasBeenTriggered = false;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (triggerCollider == null)
        {
            // Ambil collider dari gameObject ini, pastikan yang dipakai adalah trigger
            triggerCollider = GetComponent<Collider2D>();

            if (triggerCollider == null || !triggerCollider.isTrigger)
            {
                Debug.LogError($"NPC {gameObject.name} tidak memiliki Collider2D yang bertipe trigger!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            // Cek apakah karakter sudah pernah muncul sebelumnya
            if (PlayerPrefs.GetInt("CharacterSeen_" + characterData.characterID, 0) == 0)
            {
                CharacterPopUpUI.Instance.ShowCharacterDetail(characterData);
                hasBeenTriggered = true; // Mencegah pemunculan ulang selama sesi game ini
            }
        }
    }
}

using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestTriggerZoneListener : MonoBehaviour
    {
        [SerializeField] private QuestTriggerZoneStep questStep;
        private string playerTag = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag) && questStep != null)
            {
                questStep.TriggeredByPlayer();
                // Optional: gameObject.SetActive(false);
            }
        }
    }
}

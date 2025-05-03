using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestVisualCue : MonoBehaviour
    {
        [Header("Visual Cue")]
        [SerializeField] private float activationRange = 3f; // Jarak munculnya visual cue
        [SerializeField] private GameObject visualCue; // GameObject sprite/partikel cue

        [Header("Quest Condition")]
        [SerializeField] private string questId; // ID Quest yang akan dicek
        [SerializeField] private QuestState requiredState = QuestState.Inactive; // State yang dibutuhkan

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            visualCue.SetActive(false); // Matikan visual cue di awal
        }

        private void Update()
        {
            if (player == null) return;

            // Cek jarak player
            float distance = Vector2.Distance(transform.position, player.position);
            bool isInRange = distance <= activationRange;

            // Cek status quest
            bool isQuestConditionMet = CheckQuestCondition();

            // Aktifkan visual cue jika kedua kondisi terpenuhi
            visualCue.SetActive(isInRange && isQuestConditionMet);
        }

        private bool CheckQuestCondition()
        {
            Quest quest = QuestManager.Instance.GetQuestById(questId);
            return quest != null && quest.state == requiredState;
        }
    }
}

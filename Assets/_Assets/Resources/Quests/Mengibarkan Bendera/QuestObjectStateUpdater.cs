using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestObjectStateUpdater : MonoBehaviour
    {
        [System.Serializable]
        public class QuestObject
        {
            public string questId;                   // ID Quest yang dikontrol
            public GameObject[] objectsToEnable;     // GameObjects yang akan diaktifkan jika quest selesai
            public Collider2D[] collidersToDisable;  // Collider yang akan dihapus jika quest selesai
        }

        [SerializeField] private QuestObject[] questObjects;

        private void Start()
        {
            UpdateQuestObjects();  // Cek status awal saat game mulai

            // Gunakan event yang tersedia untuk mendeteksi perubahan status quest
            GameEventsManager.Instance.questEvents.onQuestStateChange += OnQuestStateChanged;
        }

        private void OnDestroy()
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange -= OnQuestStateChanged;
        }

        private void OnQuestStateChanged(Quest quest)
        {
            UpdateQuestObjects();
        }

        private void UpdateQuestObjects()
        {
            foreach (var questObject in questObjects)
            {
                Quest quest = QuestManager.Instance.GetQuestById(questObject.questId);

                if (quest != null && quest.state == QuestState.Completed)
                {
                    // Aktifkan semua GameObject yang diinginkan
                    foreach (var obj in questObject.objectsToEnable)
                    {
                        if (obj != null) obj.SetActive(true);
                    }

                    // Hapus semua Collider pada objek yang diinginkan
                    foreach (var col in questObject.collidersToDisable)
                    {
                        if (col != null) Destroy(col);
                    }
                }
            }
        }
    }
}

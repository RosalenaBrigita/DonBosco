using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Inventory;

namespace DonBosco.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests/QuestInfoSO")]
    public class QuestInfoSO : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }
        public string questName;
        public string questDescription;
        public QuestType questType;
        public QuestState initialState;
        public int event_no; //event no is the event number in the event log (from API)

        [Header("Steps")]
        public QuestStep[] questSteps;

        [Header("Settings")]
        public QuestInfoSO[] prequisiteQuests;

        [Header("Rewards")]
        public string rewardName;
        public Items[] rewardItems;

        public void ClaimRewards()
        {
            if (rewardItems == null || rewardItems.Length == 0)
            {
                Debug.LogWarning("Tidak ada reward item untuk quest ini!");
                return;
            }

            InventoryController inventoryController = GameObject.FindObjectOfType<InventoryController>();

            if (inventoryController == null)
            {
                Debug.LogWarning("InventoryController tidak ditemukan!");
                return;
            }

            InventorySO inventoryData = inventoryController.GetInventorySO();

            foreach (var reward in rewardItems)
            {
                if (reward != null)
                {
                    int remainder = inventoryData.AddItem(reward.InventoryItem, reward.Quantity);

                    if (remainder == 0)
                    {
                        Debug.Log($"{reward.Quantity} {reward.InventoryItem.name} telah ditambahkan ke inventory!");
                    }
                    else
                    {
                        Debug.LogWarning($"Tidak cukup ruang di inventory untuk {reward.InventoryItem.name}! Sisa: {remainder}");
                    }
                }
            }
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public enum QuestType
    {
        MainQuest,
        SideQuest
    }
}

using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Inventory;

namespace DonBosco.Quests
{
    public class QuestInventoryStep : QuestStep
    {
        [SerializeField] private List<QuestItemSO> requiredItems; // Daftar item yang dibutuhkan

        protected override void SetQuestStepState(string state)
        {
            // Tidak diperlukan untuk inventory quest
        }

        public bool CheckRequiredItems()
        {
            // Ambil state inventory dari InventorySO
            InventorySO inventory = InventoryController.Instance.GetInventorySO();
            Dictionary<int, InventoryItem> inventoryState = inventory.GetCurrentInventoryState();

            foreach (var requiredItem in requiredItems)
            {
                bool itemFound = false;

                // Periksa apakah inventory memiliki item yang cocok dengan yang dibutuhkan
                foreach (var item in inventoryState.Values)
                {
                    if (item.item == requiredItem && item.quantity > 0)
                    {
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                {
                    Debug.Log($"Item {requiredItem.name} belum ada di inventory.");
                    return false; // Jika salah satu item tidak ada, quest step tidak bisa selesai
                }
            }

            // Jika semua item ditemukan, bisa menyelesaikan quest step
            Debug.Log("Semua item ditemukan! Menyelesaikan quest step.");
            FinishQuestStep();
            return true;
        }
    }
}

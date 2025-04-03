using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Inventory;

namespace DonBosco.Quests
{
    public class QuestInventoryStep : QuestStep
    {
        [SerializeField] private List<QuestItemSO> requiredItems;
        private bool isQuestCompleted = false; // Flag untuk menghentikan update

        private void Update()
        {
            if (!isQuestCompleted) // Cek hanya jika quest belum selesai
            {
                CheckInventory();
            }
        }

        private void CheckInventory()
        {
            if (AreRequiredItemsPresent())
            {
                Debug.Log("[Quest] Semua item terkumpul!");
                FinishQuestStep();
                isQuestCompleted = true; // Set flag agar tidak dipanggil lagi
            }
        }

        private bool AreRequiredItemsPresent()
        {
            if (requiredItems == null || requiredItems.Count == 0)
                return true;

            if (InventoryController.Instance == null)
            {
                Debug.LogWarning("[Quest] InventoryController tidak ditemukan");
                return false;
            }

            var inventory = InventoryController.Instance.GetInventorySO();
            if (inventory == null) return false;

            var inventoryState = inventory.GetCurrentInventoryState();
            int itemsFound = 0;

            foreach (var requiredItem in requiredItems)
            {
                if (requiredItem == null) continue;

                foreach (var slot in inventoryState.Values)
                {
                    if (!slot.IsEmpty && slot.item != null && slot.item.ID == requiredItem.ID)
                    {
                        itemsFound++;
                        break;
                    }
                }
            }

            return itemsFound == requiredItems.Count;
        }

        protected override void SetQuestStepState(string state) { }
    }
}

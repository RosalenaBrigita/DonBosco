using Inventory;
using Inventory.Model;
using UnityEngine;
using System.Collections.Generic;

public class RemoveQuestItems : MonoBehaviour
{
    [SerializeField] private List<QuestItemSO> itemsToRemove;

    public void RemoveItems()
    {
        var inventory = InventoryController.Instance.GetInventorySO();
        if (inventory == null)
        {
            Debug.LogWarning("InventorySO tidak ditemukan.");
            return;
        }

        var inventoryState = inventory.GetCurrentInventoryState();

        foreach (var itemToRemove in itemsToRemove)
        {
            bool itemFound = false;

            foreach (var pair in inventoryState)
            {
                int index = pair.Key;
                InventoryItem slotItem = pair.Value;

                if (slotItem.item.ID == itemToRemove.ID && slotItem.quantity > 0)
                {
                    // Hapus 1 item
                    inventory.RemoveItem(index, 1);
                    Debug.Log("Item " + itemToRemove.name + " dihapus dari inventory.");
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                Debug.LogWarning("Item " + itemToRemove.name + " tidak ditemukan dalam inventory.");
            }
        }
    }
}
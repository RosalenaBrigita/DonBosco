using Inventory;
using Inventory.Model;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] private Items item; // Ambil data dari Item.cs

    private bool wasAdded = false; // Add this flag

    public void AddItemToInventory()
    {
        if (wasAdded) return; // Prevent double execution

        InventoryController inventoryController = GameObject.FindObjectOfType<InventoryController>();
        if (inventoryController == null)
        {
            Debug.LogWarning("InventoryController tidak ditemukan!");
            return;
        }

        InventorySO inventoryData = inventoryController.GetInventorySO();

        if (inventoryData != null && item != null)
        {
            int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (remainder == 0)
            {
                wasAdded = true; // Mark as added
                Debug.Log(item.Quantity + " " + item.InventoryItem.name + " telah ditambahkan ke inventory!");

                inventoryController.ShowItemAddedPanel(item.InventoryItem);
            }
            else
            {
                Debug.LogWarning("Tidak cukup ruang di inventory! Sisa: " + remainder);
            }
        }
        else
        {
            Debug.LogWarning("InventoryData atau Item belum diassign!");
        }
    }

    // Reset when needed
    public void ResetAddStatus()
    {
        wasAdded = false;
    }
}

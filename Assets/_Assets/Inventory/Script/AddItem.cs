using Inventory;
using Inventory.Model;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] private Items item; // Ambil data dari Item.cs

    public void AddItemToInventory()
    {
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
                Debug.Log(item.Quantity + " " + item.InventoryItem.name + " telah ditambahkan ke inventory!");
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
}

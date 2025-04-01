/*using Inventory.Model;
using UnityEngine;

public class SewingResultHandler : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private Item item; // Ambil data dari Item.cs

    public void AddItemToInventory()
    {
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
*/
using UnityEngine;
using Inventory.Model;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventorySO inventoryData; // Assign di Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hapus saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

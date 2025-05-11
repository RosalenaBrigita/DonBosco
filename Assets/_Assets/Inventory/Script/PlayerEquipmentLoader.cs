using UnityEngine;
using DonBosco.SaveSystem;

public class PlayerEquipmentLoader : MonoBehaviour
{
    private void Start()
    {
        LoadEquipment();
    }

    public void LoadEquipment()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.LoadEquippedItems(gameObject);
        }
        else
        {
            Debug.LogWarning("SaveManager belum diinisialisasi");
        }
    }
}
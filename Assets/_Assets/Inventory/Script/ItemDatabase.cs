using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] private List<EquippableItemSO> equippableItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemSO GetItemByID(int id)
    {
        foreach (var item in equippableItems)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }
}
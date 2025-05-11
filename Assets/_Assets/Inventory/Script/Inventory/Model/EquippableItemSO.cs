using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco.SaveSystem;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Items/Equippable Item")]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] 
        private GameObject itemPrefab;
        public GameObject ItemPrefab => itemPrefab;

        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            if (itemPrefab == null)
            {
                Debug.LogWarning($"ItemPrefab kosong pada {Name}");
                return false;
            }

            Transform targetParent = character.transform.Find("Visual") ?? character.transform;
            Instantiate(itemPrefab, targetParent);
            
            SaveManager.Instance.SaveEquippedItem(ID, targetParent.name);
            Debug.Log($"{Name} (ID:{ID}) di-equip ke {targetParent.name}");
            
            return true;
        }
    }
}
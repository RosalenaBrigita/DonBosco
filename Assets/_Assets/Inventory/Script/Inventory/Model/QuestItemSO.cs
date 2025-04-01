using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/EventItem")]
    public class QuestItemSO : ItemSO
    {
        [field: SerializeField]
        public string questName { get; private set; }  // Nama quest yang membutuhkan item ini

        public bool CanBeUsedInQuest(string currentQuest)
        {
            return questName == currentQuest;
        }
    }
}

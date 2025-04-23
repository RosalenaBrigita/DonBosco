using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        public float healAmount = 10f;

        public string ActionName => "Consume";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            // Get the player events manager and trigger healing
            if (GameEventsManager.Instance != null)
            {
                GameEventsManager.Instance.playerEvents.PlayerHeal(healAmount);
                return true;
            }
            return false;
        }
    }

    public interface IDestroyableItem
    {
        // Marker interface for items that are destroyed on use
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}
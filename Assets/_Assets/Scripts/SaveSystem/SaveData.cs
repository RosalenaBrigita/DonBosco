using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using DonBosco.ItemSystem;
using DonBosco.Quests;

namespace DonBosco
{
    [System.Serializable]
    public class SaveData
    {
        #region Player Data
        public Vector3 playerPosition;
        public string currentScene;
        public float playerHealth;
        //public ItemData[] playerInventory;
        #endregion


        #region Game Data
        public Vector2 playerIconPosition;
        public string dialogueVariables = null;
        public List<string> seenCharacterIDs = new List<string>();
        #endregion

        #region Quest

        public QuestData[] questData;
        #endregion


        #region Quiz
        public int[] quizAnswers;
        #endregion

        #region Inventory
        public List<InventoryItem> inventoryItems;
        #endregion

        #region Object
        public List<ObjectStateData> objectStates = new List<ObjectStateData>();
        public List<EquippedItemData> equippedItems = new List<EquippedItemData>();
        public List<ActivationStateData> activationStates = new List<ActivationStateData>();
        #endregion 

        #region Analytics
        public float timeSpentInGame;
        #endregion


        #region Progression Data
        #endregion
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class ObjectStateData
    {
        public string objectID;
        public bool isDisabled;

        public ObjectStateData(string id, bool disabled)
        {
            objectID = id;
            isDisabled = disabled;
        }
    }

    [System.Serializable]
    public class ActivationStateData
    {
        public string objectID;
        public bool isActivated; // Status aktif/nonaktif GameObject
        public bool isColliderActive; // Status Collider2D

        public ActivationStateData(string id, bool activated, bool colliderActive)
        {
            objectID = id;
            isActivated = activated;
            isColliderActive = colliderActive;
        }
    }

    [System.Serializable]
    public class EquippedItemData
        {
            public int itemID;
            public string parentPath;
        }

}

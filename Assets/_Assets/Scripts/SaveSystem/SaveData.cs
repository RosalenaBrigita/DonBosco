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
        public ItemData[] playerInventory;
        #endregion


        #region Game Data
        public string dialogueVariables = null;
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
}

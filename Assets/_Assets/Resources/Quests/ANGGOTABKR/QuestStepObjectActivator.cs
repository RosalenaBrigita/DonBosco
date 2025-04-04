using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestStepObjectActivator : MonoBehaviour
    {
        [System.Serializable]
        public class StepConditionObject
        {
            public string questId;
            public int requiredStepIndex;
            public GameObject[] objectsToActivate;
        }

        [SerializeField] private StepConditionObject[] stepObjects;

        private void Start()
        {
            UpdateStepObjects();
        }

        private void UpdateStepObjects()
        {
            foreach (var stepObject in stepObjects)
            {
                Quest quest = QuestManager.Instance.GetQuestById(stepObject.questId);
                bool shouldActivate = quest != null && quest.currentStepIndex == stepObject.requiredStepIndex;

                foreach (var obj in stepObject.objectsToActivate)
                {
                    if (obj != null)
                        obj.SetActive(shouldActivate);
                }
            }
        }
    }
}

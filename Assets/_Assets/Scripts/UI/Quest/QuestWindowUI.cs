using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DonBosco.Quests;
using System.Collections.Generic;

namespace DonBosco.UI
{
    public class QuestWindowUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject contentParent = null;
        [SerializeField] private GameObject questContentPrefab = null;

        [Header("Detail Panel")]
        [SerializeField] private GameObject questDetailPanel = null;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TMP_Text questNameText = null;
        [SerializeField] private TMP_Text questDescriptionText = null; // Changed from questDescriptionText
        [SerializeField] private TMP_Text questRewardText = null;

        private Dictionary<string, QuestContentUI> questContentUIs = new Dictionary<string, QuestContentUI>();
        private Quest currentDisplayedQuest;

        void OnEnable()
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange += OnQuestStateChange;
        }

        void OnDisable()
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange -= OnQuestStateChange;
        }

        private void OnQuestStateChange(Quest quest)
        {
            if (questContentUIs.ContainsKey(quest.info.id))
            {
                questContentUIs[quest.info.id].QuestStateChange(quest);
            }
            else
            {
                InstantiateQuestContent(quest);
                questContentUIs[quest.info.id].QuestStateChange(quest);
            }
        }

        private void InstantiateQuestContent(Quest quest)
        {
            GameObject questContent = Instantiate(questContentPrefab, contentParent.transform);
            QuestContentUI questContentUI = questContent.GetComponent<QuestContentUI>();
            questContentUI.SetContent(quest, this);
            questContentUIs.Add(quest.info.id, questContentUI);
        }

        public void ShowQuestDetails(Quest quest)
        {
            if (questDetailPanel != null && quest != null)
            {
                questDetailPanel.SetActive(true);
                questNameText.text = quest.info.questName;

                // Get current step info
                int currentStepIndex = quest.currentStepIndex;
                QuestStep currentStep = quest.info.questSteps[currentStepIndex];
                QuestStepInfoSO stepInfo = currentStep.questStepInfo;

                // Format description text
                string formattedDescription = $"<color=#000000><b><u>{stepInfo.taskName}:</u></b></color>\n{stepInfo.taskDescription}";
                questRewardText.text = quest.info.rewardName;

                // Reset scroll position
                scrollRect.normalizedPosition = new Vector2(0, 1);
                UpdateScrollbarVisibility();
            }
        }

        private void UpdateScrollbarVisibility()
        {
            // Force layout update biar ukuran konten benar
            LayoutRebuilder.ForceRebuildLayoutImmediate(questDescriptionText.rectTransform);

            float contentHeight = questDescriptionText.rectTransform.rect.height;
            float viewportHeight = scrollRect.viewport.rect.height;

            bool needsScroll = contentHeight > viewportHeight;
            scrollRect.verticalScrollbar.gameObject.SetActive(needsScroll);
        }

        private string GetCurrentStepDescription(Quest quest)
        {
            if (quest.currentStepIndex >= 0 &&
                quest.currentStepIndex < quest.info.questSteps.Length)
            {
                QuestStep currentStep = quest.info.questSteps[quest.currentStepIndex];
                return currentStep.questStepInfo != null ?
                    currentStep.questStepInfo.taskDescription :
                    "No step description available";
            }
            return "Quest step not found";
        }

        public void CloseQuestDetails()
        {
            if (questDetailPanel != null)
            {
                questDetailPanel.SetActive(false);
                currentDisplayedQuest = null;
            }
        }

        // Call this when quest step progresses
        public void RefreshCurrentStep()
        {
            if (currentDisplayedQuest != null && questDetailPanel.activeSelf)
            {
                string stepDescription = GetCurrentStepDescription(currentDisplayedQuest);
                questDescriptionText.text = stepDescription;
                scrollRect.normalizedPosition = new Vector2(0, 1);
            }
        }
    }
}
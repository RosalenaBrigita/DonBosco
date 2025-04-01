using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DonBosco.Quests;
using System;

namespace DonBosco.UI
{
    public class QuestWindowUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject contentParent = null;
        [SerializeField] private GameObject questContentPrefab = null;

        // Panel Detail
        [SerializeField] private GameObject questDetailPanel = null; 
        [SerializeField] private TMP_Text questNameText = null;
        [SerializeField] private TMP_Text questDescriptionText = null;
        [SerializeField] private TMP_Text questRewardText = null;

        private Dictionary<string, QuestContentUI> questContentUIs = new Dictionary<string, QuestContentUI>();


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
            questContentUI.SetContent(quest, this); // Kirim referensi QuestWindowUI
            questContentUIs.Add(quest.info.id, questContentUI);
        }

        public void ShowQuestDetails(Quest quest)
        {
            if (questDetailPanel != null && quest != null)
            {
                questDetailPanel.SetActive(true);
                questNameText.text = quest.info.questName;
                questDescriptionText.text = quest.info.questDescription;
                questRewardText.text = quest.info.rewardName;
            }
        }

        public void CloseQuestDetails()
        {
            if (questDetailPanel != null)
            {
                questDetailPanel.SetActive(false);
            }
        }
    }

}
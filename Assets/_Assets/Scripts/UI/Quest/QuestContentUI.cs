using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DonBosco.Quests;

namespace DonBosco.UI
{
    public class QuestContentUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text titleText = null;
        [SerializeField] private TMP_Text descriptionText = null;
        [SerializeField] private Button questButton = null; // Tambahkan button

        private Quest quest = null;
        private QuestWindowUI questWindowUI; // Referensi ke QuestWindowUI

        public void SetContent(Quest quest, QuestWindowUI questWindow)
        {
            this.quest = quest;
            this.questWindowUI = questWindow;
            QuestStateChange(quest);

            // Pastikan button bisa diklik
            if (questButton != null)
            {
                questButton.onClick.AddListener(() => questWindowUI.ShowQuestDetails(quest));
            }
        }

        public void QuestStateChange(Quest quest)
        {
            if (this.quest == quest)
            {
                switch (quest.state)
                {
                    case QuestState.Inactive:
                        gameObject.SetActive(false);
                        break;
                    case QuestState.Active:
                        OnInProgress();
                        break;
                    case QuestState.CanFinish:
                        OnCanFinish();
                        break;
                    case QuestState.Completed:
                        OnComplete();
                        break;
                }
            }
        }

        private void OnInProgress()
        {
            gameObject.SetActive(true);
            titleText.text = quest.info.questName;
            int currentStepIndex = quest.currentStepIndex;
            string description = quest.info.questSteps[currentStepIndex].questStepInfo.taskName;
            descriptionText.text = description;
        }

        private void OnCanFinish()
        {
            gameObject.SetActive(true);
            titleText.text = quest.info.questName;
            int currentStepIndex = quest.currentStepIndex;
            string description = quest.info.questSteps[currentStepIndex - 1].questStepInfo.taskName;
            descriptionText.text = "<s>" + description + "</s>";
        }

        private void OnComplete()
        {
            gameObject.SetActive(false);
        }
    }
}

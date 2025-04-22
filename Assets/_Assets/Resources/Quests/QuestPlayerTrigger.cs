using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestPlayerTrigger : QuestStep
    {
        public void EnterTrigger()
        {
            FinishQuestStep();
        }

        public void FinishQuest()
        {
            InstantFinishQuest();
        }

        protected override void SetQuestStepState(string state)
        {
            // Do nothing
        }
    }

}
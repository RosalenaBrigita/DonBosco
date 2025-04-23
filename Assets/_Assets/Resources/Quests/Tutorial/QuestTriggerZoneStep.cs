using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestTriggerZoneStep : QuestStep
    {
        public void TriggeredByPlayer()
        {
            //Debug.Log("YEAH");
            FinishQuestStep();
        }

        protected override void SetQuestStepState(string state)
        {
            // Tidak perlu apa-apa
        }
    }
}

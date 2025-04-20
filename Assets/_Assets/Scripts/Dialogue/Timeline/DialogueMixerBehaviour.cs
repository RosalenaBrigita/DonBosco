using UnityEngine;
using UnityEngine.Playables;

namespace DonBosco.Dialogue
{
    public class DialogueMixerBehaviour : PlayableBehaviour
    {
        private int m_FirstFrameHappened = -1;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var timeline = playerData as DialogueTimeline;
            if (timeline == null) return;

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0.5f)
                {
                    if (m_FirstFrameHappened == i) return;

                    Debug.Log($"Attempting to trigger dialogue for clip {i}");

                    var inputPlayable = (ScriptPlayable<StartDialogueBehaviour>)playable.GetInput(i);
                    var behaviour = inputPlayable.GetBehaviour();

                    if (behaviour.IsReady())
                    {
                        behaviour.TriggerDialogue();
                        m_FirstFrameHappened = i;
                    }
                }
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            m_FirstFrameHappened = -1;
        }
    }
}
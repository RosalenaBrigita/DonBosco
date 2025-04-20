using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

namespace DonBosco.Dialogue
{
    public class StartDialogueBehaviour : PlayableBehaviour
    {
        public int npcID;
        public string knotPath;
        private bool isLoaded = false;
        private TextAsset loadedDialogue;

        public bool IsReady() => isLoaded && loadedDialogue != null;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!isLoaded)
            {
                var timeline = Object.FindObjectOfType<DialogueTimeline>();
                if (timeline != null)
                {
                    timeline.StartCoroutine(LoadDialogue(timeline));
                }
            }
            else if (loadedDialogue != null)
            {
                TriggerDialogue();
            }
        }

        private IEnumerator LoadDialogue(DialogueTimeline timeline)
        {
            Debug.Log($"Starting to load dialogue for NPC {npcID}");

            yield return NPCToDialogueConverter.GetDialogueFromNPC(npcID, (dialogue) =>
            {
                if (dialogue == null)
                {
                    Debug.LogError($"Failed to load dialogue for NPC {npcID}");
                    return;
                }

                loadedDialogue = dialogue;
                isLoaded = true;
                Debug.Log($"Successfully loaded dialogue for NPC {npcID}");

                TriggerDialogue();
            });
        }

        public void TriggerDialogue()
        {
            if (loadedDialogue == null)
            {
                Debug.LogWarning($"Dialogue for NPC {npcID} not loaded yet");
                return;
            }

            var timeline = Object.FindObjectOfType<DialogueTimeline>();
            if (timeline != null)
            {
                timeline.StartDialogue(loadedDialogue, knotPath);
            }
        }
    }
}
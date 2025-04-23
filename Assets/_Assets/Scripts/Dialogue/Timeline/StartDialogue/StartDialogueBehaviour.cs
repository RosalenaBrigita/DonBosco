using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

namespace DonBosco.Dialogue
{
    public class StartDialogueBehaviour : PlayableBehaviour
    {
        public TextAsset localDialogue; // Fallback dialogue
        public int npcID;
        public string knotPath;
        private bool isLoaded = false;
        private TextAsset loadedDialogue;

        public bool IsReady() => isLoaded || localDialogue != null;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!isLoaded)
            {
                var timeline = Object.FindObjectOfType<DialogueTimeline>();
                if (timeline != null)
                {
                    timeline.StartCoroutine(TryLoadDialogue(timeline));
                }
            }
            else if (IsReady())
            {
                TriggerDialogue();
            }
        }

        private IEnumerator TryLoadDialogue(DialogueTimeline timeline)
        {
            // Attempt to load from database first
            yield return NPCToDialogueConverter.GetDialogueFromNPC(npcID, (databaseDialogue) =>
            {
                if (databaseDialogue != null)
                {
                    loadedDialogue = databaseDialogue;
                    Debug.Log($"Successfully loaded dialogue from DB for NPC {npcID}");
                }
                else
                {
                    loadedDialogue = localDialogue;
                    Debug.LogWarning($"Falling back to local dialogue for NPC {npcID}");
                }

                isLoaded = true;
                TriggerDialogue();
            });
        }

        public void TriggerDialogue()
        {
            var dialogueToUse = loadedDialogue ?? localDialogue;

            if (dialogueToUse == null)
            {
                Debug.LogError($"No dialogue available for NPC {npcID}");
                return;
            }

            var timeline = Object.FindObjectOfType<DialogueTimeline>();
            timeline?.StartDialogue(dialogueToUse, knotPath);
        }
    }
}
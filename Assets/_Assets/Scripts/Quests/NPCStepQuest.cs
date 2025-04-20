using UnityEngine;
using DonBosco.Character.NPC.Test;
using DonBosco.Dialogue;
using DonBosco.ItemSystem;
using UnityEngine.Networking;
using System.Collections;

namespace DonBosco.Quests
{
    public class NPCQuestStep : QuestStep, IInteractable
    {
        [Header("Dialogue Sources")]
        [SerializeField] protected TextAsset dialogue; // Fallback dialogue
        [SerializeField] protected int npcId; // For database lookup
        [SerializeField] protected bool useDatabaseDialogue = true;

        [Header("Dialogue Configuration")]
        [SerializeField] protected DialogueQuestConversation[] dialogueQuestConversations;
        [SerializeField] protected ConversationState[] conversationStates;

        private DialogueQuestConversation currentDialogueQuestConversation;
        private ConversationState currentState;
        private bool dialogueLoaded = false;
        private string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";

        public bool IsInteractable { get; set; } = true;

        public virtual void Interact()
        {
            StartDialogue();
        }

        public virtual void Interact(Item item)
        {
            StartDialogue();
        }

        private void StartDialogue()
        {
            Debug.Log($"StartDialogue called - useDB: {useDatabaseDialogue}, loaded: {dialogueLoaded}");

            if (useDatabaseDialogue && !dialogueLoaded)
            {
                Debug.Log($"Attempting to load dialogue for NPC {npcId} from server");
                StartCoroutine(LoadDialogueFromServer());
                return;
            }
            else
            {
                Debug.Log($"Using local dialogue asset: {(dialogue != null ? dialogue.name : "NULL")}");
            }

            ProceedWithDialogue();
        }

        private IEnumerator LoadDialogueFromServer()
        {
            string url = $"{dialogueUrl}?npc_id={npcId}";
            Debug.Log($"Loading from URL: {url}");

            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            Debug.Log($"Server response: {www.result}, Status: {www.responseCode}");

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Raw response: {www.downloadHandler.text}");

                NPCDialogueResponse response = JsonUtility.FromJson<NPCDialogueResponse>(www.downloadHandler.text);
                if (!string.IsNullOrEmpty(response.ink_json))
                {
                    dialogue = new TextAsset(response.ink_json);
                    dialogueLoaded = true;
                    Debug.Log($"Successfully loaded dialogue for NPC {npcId}. Text length: {response.ink_json.Length} chars");
                }
                else
                {
                    Debug.LogWarning("Server returned empty ink_json");
                }
            }
            else
            {
                Debug.LogError($"Failed to load dialogue: {www.error}");
            }

            ProceedWithDialogue();
        }

        private void ProceedWithDialogue()
        {
            Debug.Log($"Proceeding with dialogue - Asset: {(dialogue != null ? "Exists" : "NULL")}");

            if (dialogue == null)
            {
                Debug.LogError("Critical: No dialogue asset available");
                return;
            }

            GetCurrentState();

            if (currentDialogueQuestConversation != null)
            {
                StartDialogueQuestConversation();
            }
            else if (currentState.knotPath != null)
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue, currentState.knotPath);
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue);
            }
        }

        private void StartDialogueQuestConversation()
        {
            foreach (var action in currentDialogueQuestConversation.questStepActions)
            {
                switch (action.dialogueQuestBehaviour)
                {
                    case DialogueQuestBehaviour.StartQuest:
                        GameEventsManager.Instance.questEvents.StartQuest(action.questStepInfo.questInfo.id);
                        break;
                    case DialogueQuestBehaviour.AdvanceQuest:
                        GameEventsManager.Instance.questEvents.AdvanceQuest(action.questStepInfo.questInfo.id);
                        break;
                    case DialogueQuestBehaviour.FinishQuest:
                        GameEventsManager.Instance.questEvents.FinishQuest(action.questStepInfo.questInfo.id);
                        break;
                }
            }

            DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;

            if (currentDialogueQuestConversation.knotPath != null)
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue, currentDialogueQuestConversation.knotPath);
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue);
            }
        }

        private void OnDialogueEnded()
        {
            DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
            currentDialogueQuestConversation?.onDialogueDone?.Invoke();
        }

        private void GetCurrentState()
        {
            currentDialogueQuestConversation = null;

            for (int i = 0; i < dialogueQuestConversations.Length; i++)
            {
                DialogueQuestConversation dialogueQuestConversation = dialogueQuestConversations[i];
                if (CheckQuestConditions(dialogueQuestConversation))
                {
                    currentDialogueQuestConversation = dialogueQuestConversation;
                    break;
                }
            }

            if (currentDialogueQuestConversation != null || conversationStates == null || conversationStates.Length == 0)
            {
                return;
            }

            for (int i = 0; i < conversationStates.Length; i++)
            {
                ConversationState conversationState = conversationStates[i];
                if (CheckQuestConditions(conversationState))
                {
                    currentState = conversationState;
                    break;
                }
            }
        }

        private bool CheckQuestConditions(DialogueQuestConversation dqc)
        {
            for (int i = 0; i < dqc.conditions.Length; i++)
            {
                QuestCondition questCondition = dqc.conditions[i];
                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);

                if (quest.state == questCondition.questState && quest.currentStepIndex == questCondition.questStepIndex)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckQuestConditions(ConversationState conversationState)
        {
            for (int i = 0; i < conversationState.questConditions.Length; i++)
            {
                QuestCondition questCondition = conversationState.questConditions[i];
                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);

                if (quest.state != questCondition.questState || quest.currentStepIndex != questCondition.questStepIndex)
                {
                    return false;
                }
            }
            return true;
        }

        protected override void SetQuestStepState(string state)
        {
        }

        [System.Serializable]
        private class NPCDialogueResponse
        {
            public string ink_json;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco.Dialogue;
using DonBosco.Character.NPC.Test;
using System;
using UnityEngine.Events;
using DonBosco.ItemSystem;

namespace DonBosco.Quests
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueQuest : MonoBehaviour, IInteractable
    {
        [SerializeField] protected TextAsset dialogue;
        [SerializeField] protected DialogueQuestConversation[] dialogueQuestConversations;
        [SerializeField] protected ConversationState[] conversationStates;
        [SerializeField] protected ExternalFunction[] externalFunctions;

        private DialogueQuestConversation currentDialogueQuestConversation;
        private ConversationState currentState;

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
            if (dialogue == null)
            {
                Debug.LogError("No dialogue or knot path assigned to NPC");
            }
            else
            {
                GetCurrentState();

                // Bind external functions
                if (externalFunctions != null)
                {
                    for (int i = 0; i < externalFunctions.Length; i++)
                    {
                        ExternalFunction externalFunction = externalFunctions[i];
                        DialogueManager.GetInstance().BindExternalFunction(externalFunction.functionName, (param) => externalFunction.onFunctionCalled?.Invoke(param));
                    }
                }

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
        }

        private void StartDialogueQuestConversation()
        {
            switch (currentDialogueQuestConversation.dialogueQuestBehaviour)
            {
                case DialogueQuestBehaviour.StartQuest:
                    GameEventsManager.Instance.questEvents.StartQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
                case DialogueQuestBehaviour.AdvanceQuest:
                    GameEventsManager.Instance.questEvents.AdvanceQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
                case DialogueQuestBehaviour.FinishQuest:
                    GameEventsManager.Instance.questEvents.FinishQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
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
            currentDialogueQuestConversation.onDialogueDone?.Invoke();

            // Cari komponen AddItem di objek yang sama
            AddItem addItemComponent = GetComponent<AddItem>();
            if (addItemComponent != null)
            {
                addItemComponent.AddItemToInventory();
            }
            else
            {
                Debug.LogWarning("AddItem tidak ditemukan pada objek ini.");
            }
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
    }

    [System.Serializable]
    public class DialogueQuestConversation
    {
        public QuestCondition[] conditions;
        public string knotPath;
        public QuestStepInfoSO questStepInfo;
        public DialogueQuestBehaviour dialogueQuestBehaviour;
        public UnityEvent onDialogueDone;
    }

    [System.Serializable]
    public enum DialogueQuestBehaviour
    {
        StartQuest,
        AdvanceQuest,
        FinishQuest,
    }

    [System.Serializable]
    public class ExternalFunction
    {
        public string functionName;
        public UnityEvent<string> onFunctionCalled;
    }
}

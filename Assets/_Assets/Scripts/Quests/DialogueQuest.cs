using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco.Dialogue;
using DonBosco.Character.NPC.Test;
using System;
using UnityEngine.Events;
using DonBosco.ItemSystem;
using UnityEngine.Networking;

namespace DonBosco.Quests
{
    public class DialogueQuest : MonoBehaviour, IInteractable
    {
        [SerializeField] protected TextAsset dialogue;
        [SerializeField] private int npcID;
        
        [SerializeField] protected DialogueQuestConversation[] dialogueQuestConversations;
        [SerializeField] protected ConversationState[] conversationStates;
        [SerializeField] protected ExternalFunction[] externalFunctions;

        private Collider2D _collider;
        private DialogueQuestConversation currentDialogueQuestConversation;
        private ConversationState currentState;

        private string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";
        private bool dialogueLoaded = false;

        public bool IsInteractable { get; set; } = true;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();

            // Jika collider tidak ada, tambahkan Collider2D secara otomatis
            if (_collider == null)
            {
                _collider = gameObject.AddComponent<BoxCollider2D>();
                Debug.Log("DialogueQuest: Collider2D otomatis ditambahkan karena tidak ada.");
            }
        }

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
            if (!dialogueLoaded)
            {
                StartCoroutine(LoadDialogueFromServer());
                return;
            }

            ProceedDialogue();
        }

        IEnumerator LoadDialogueFromServer()
        {
            string url = $"{dialogueUrl}?npc_id={npcID}";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error load dialogue: " + www.error);
            }
            else
            {
                InkWrapper result = JsonUtility.FromJson<InkWrapper>(www.downloadHandler.text);
                if (!string.IsNullOrEmpty(result.ink_json))
                {
                    dialogue = new TextAsset(result.ink_json);
                    dialogueLoaded = true;
                    ProceedDialogue();
                }
            }
        }

        [Serializable]
        private class InkWrapper
        {
            public string ink_json;
        }

        private void ProceedDialogue()
        {
            GetCurrentState();

            if (externalFunctions != null)
            {
                foreach (var ef in externalFunctions)
                {
                    DialogueManager.GetInstance().BindExternalFunction(ef.functionName, (param) => ef.onFunctionCalled?.Invoke(param));
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

        private void StartDialogueQuestConversation()
        {
            foreach (var questStepAction in currentDialogueQuestConversation.questStepActions)
            {
                switch (questStepAction.dialogueQuestBehaviour)
                {
                    case DialogueQuestBehaviour.StartQuest:
                        GameEventsManager.Instance.questEvents.StartQuest(questStepAction.questStepInfo.questInfo.id);
                        break;
                    case DialogueQuestBehaviour.AdvanceQuest:
                        GameEventsManager.Instance.questEvents.AdvanceQuest(questStepAction.questStepInfo.questInfo.id);
                        break;
                    case DialogueQuestBehaviour.FinishQuest:
                        GameEventsManager.Instance.questEvents.FinishQuest(questStepAction.questStepInfo.questInfo.id);
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
        public List<QuestStepAction> questStepActions; // Menggunakan List dari pasangan (QuestStepInfoSO, DialogueQuestBehaviour)
        public UnityEvent onDialogueDone;
    }

    [System.Serializable]
    public class QuestStepAction
    {
        public QuestStepInfoSO questStepInfo;
        public DialogueQuestBehaviour dialogueQuestBehaviour;
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

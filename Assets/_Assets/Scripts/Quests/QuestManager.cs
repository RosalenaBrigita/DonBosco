using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;
using DonBosco.API;

namespace DonBosco.Quests
{
    public class QuestManager : MonoBehaviour, ISaveLoad
    {
        private static QuestManager instance = null;
        public static QuestManager Instance { get { return instance; } }

        private Dictionary<string, Quest> questMap = new Dictionary<string, Quest>();
        private Dictionary<string, QuestData> loadedQuests = new Dictionary<string, QuestData>();
        private QuestInfoSO[] allQuests;
        private List<GameObject> spawnedQuestSteps = new List<GameObject>();


        #region MonoBehaviour
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        void Start()
        {
            // Hanya buat questMap kalau belum ada
            if (questMap == null || questMap.Count == 0)
            {
                questMap = CreateQuestMap();
            }
        }

        void OnEnable()
        {
            SaveManager.Instance.Subscribe(this);

            GameEventsManager.Instance.questEvents.onStartQuest += StartQuest;
            GameEventsManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
            GameEventsManager.Instance.questEvents.onFinishQuest += FinishQuest;
            GameEventsManager.Instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

            GameEventsManager.Instance.playerEvents.onChangeScene += ChangeScene;

            GameEventsManager.Instance.miscEvents.onChangeData += LoadQuestData;
        }

        void OnDisable()
        {
            SaveManager.Instance.Unsubscribe(this);

            GameEventsManager.Instance.questEvents.onStartQuest -= StartQuest;
            GameEventsManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
            GameEventsManager.Instance.questEvents.onFinishQuest -= FinishQuest;
            GameEventsManager.Instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

            GameEventsManager.Instance.playerEvents.onChangeScene -= ChangeScene;

            GameEventsManager.Instance.miscEvents.onChangeData -= LoadQuestData;

            foreach (GameObject questStep in spawnedQuestSteps)
            {
                Destroy(questStep);
            }
        }

        #endregion



        #region Events
        public void StartQuest(string id)
        {
            Quest quest = GetQuestById(id);
            spawnedQuestSteps.Add(quest.InstantiateCurrentQuestStep());
            ChangeQuestState(quest.info.id, QuestState.Active);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestById(id);

            // move on to the next step
            quest.MoveToNextStep();

            // if there are more steps, instantiate the next one
            if (quest.CurrentStepExists())
            {
                spawnedQuestSteps.Add(quest.InstantiateCurrentQuestStep());
                GameEventsManager.Instance.questEvents.QuestStateChange(quest);
            }
            // if there are no more steps, then we've finished all of them for this quest
            else
            {
                ChangeQuestState(quest.info.id, QuestState.CanFinish);
            }
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ChangeQuestState(quest.info.id, QuestState.Completed);
            // Pastikan quest memiliki informasi sebelum klaim reward
            if (quest != null && quest.info != null)
            {
                quest.info.ClaimRewards();
            }
            else
            {
                Debug.LogWarning("Quest atau QuestInfo tidak ditemukan untuk quest ID: " + id);
            }

        }

        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }


        private void ChangeScene(string sceneName)
        {
            // destroy all quest steps
            DestroyAllQuestSteps();

            foreach (Quest quest in questMap.Values)
            {
                // initialize any loaded quest steps
                if (quest.state == QuestState.Active)
                {
                    spawnedQuestSteps.Add(quest.InstantiateCurrentQuestStep());
                }
                // broadcast the initial state of all quests on startup
                GameEventsManager.Instance.questEvents.QuestStateChange(quest);
            }
        }


        private async void LoadQuestData(SaveData data)
        {
            if (data.questData != null)
            {
                await Load(data);
            }
        }
        #endregion


        /*public bool CanClaimReward(string questId)
        {
            Quest quest = GetQuestById(questId);
            if (quest == null)
            {
                Debug.LogError("Quest with id " + questId + " does not exist");
                return false;
            }

            if (quest.state == QuestState.Completed)
            {
                return true;
            }

            Debug.LogError("Quest with id " + questId + " is not completed");
            return false;
        }*/

        public Dictionary<string, Quest> CreateQuestMap()
        {
            Debug.Log("Creating Quest Map");
            // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
            allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
            // Create the quest map
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
            }
            return idToQuestMap;
        }


        private Quest LoadQuest(QuestInfoSO questInfo)
        {
            Quest quest = null;
            try
            {
                if (loadedQuests.ContainsKey(questInfo.id))
                {
                    quest = new Quest(questInfo, loadedQuests[questInfo.id]);
                }
                else
                {
                    quest = new Quest(questInfo);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading quest: " + questInfo.id + "\n" + e);
            }
            return quest;
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);
            quest.state = state;
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }

        public bool CheckRequirementsMet(string id)
        {
            Quest quest = GetQuestById(id);
            if (quest.info.prequisiteQuests.Length == 0) return true;
            foreach (QuestInfoSO prequisiteQuest in quest.info.prequisiteQuests)
            {
                if (GetQuestById(prequisiteQuest.id).state != QuestState.Completed)
                {
                    return false;
                }
            }
            return true;
        }

        public Quest GetQuestById(string id)
        {
            if (questMap.ContainsKey(id))
            {
                return questMap[id];
            }
            return null;
        }

        public void ForceResetAllQuests()
        {
            loadedQuests.Clear();
            DestroyAllQuestSteps();
            
            // Reset setiap quest individual
            foreach(var quest in questMap.Values)
            {
                quest.ForceReset(); // Panggil method reset baru
                GameEventsManager.Instance.questEvents.QuestStateChange(quest);
            }
            
            Debug.Log("All quests hard reset completed");
        }

        public void DestroyAllQuestSteps()
        {
            if (spawnedQuestSteps == null) return;

            foreach (GameObject questStep in spawnedQuestSteps)
            {
                Destroy(questStep);
            }
            spawnedQuestSteps.Clear();
        }

        #region SaveLoad
        public async Task Save(SaveData saveData)
        {
            saveData.questData = new QuestData[questMap.Count];
            int i = 0;
            foreach (Quest quest in questMap.Values)
            {
                saveData.questData[i] = quest.GetQuestData();
                i++;
            }
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            loadedQuests.Clear();
            if (saveData != null)
            {
                for (int i = 0; i < saveData.questData.Length; i++)
                {
                    loadedQuests.Add(saveData.questData[i].questId, saveData.questData[i]);
                }
            }

            questMap = CreateQuestMap();
            await Task.CompletedTask;
            GetQuestEventsLog();
        }
        #endregion



        #region API
        /// <summary>
        /// Convert the quest map to an array of EventLog to be sent to the API
        /// </summary>
        /// <returns>An array of EventLog of current quest states</returns>
        public EventLog[] GetQuestEventsLog()
        {
            EventLog[] eventLogs = new EventLog[questMap.Count];
            int i = 0;
            foreach (Quest quest in questMap.Values)
            {
                EventStatus status;
                switch (quest.state)
                {
                    case QuestState.Inactive:
                        status = EventStatus.belum;
                        break;
                    case QuestState.Active:
                        status = EventStatus.sedang;
                        break;
                    case QuestState.CanFinish:
                        status = EventStatus.sedang;
                        break;
                    case QuestState.Completed:
                        status = EventStatus.selesai;
                        break;
                    default:
                        status = EventStatus.belum;
                        break;
                }
                int thisGameId = APIManager.ID_GAME;
                // Insert the event log to the array
                eventLogs[i] = new EventLog(thisGameId, quest.info.event_no, status);
                i++;
            }
            return eventLogs;
        }
        #endregion
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco.Quests;

namespace DonBosco
{
    public class MapTransporter : MonoBehaviour
    {
        [SerializeField] private TeleportInfo[] teleportInfos;
        [SerializeField] private TeleportInfo defaultTeleportInfo;
        [SerializeField] private QuestCondition[] defaultQuestConditions; // Kondisi untuk default teleport
        [SerializeField] private GameObject warningPanel; // Panel peringatan jika kondisi tidak terpenuhi

        public void TransportPlayer()
        {
            for (int i = 0; i < teleportInfos.Length; i++)
            {
                if (CheckQuestConditions(teleportInfos[i]))
                {
                    ExecuteTeleport(teleportInfos[i]);
                    return;
                }
            }

            // Jika tidak ada teleport spesifik yang terpenuhi, cek apakah default teleport bisa digunakan
            if (CheckDefaultQuestConditions())
            {
                ExecuteTeleport(defaultTeleportInfo);
            }
            else
            {
                ShowWarningPanel(); // Tampilkan peringatan jika kondisi tidak terpenuhi
            }
        }

        private void ExecuteTeleport(TeleportInfo teleportInfo)
        {
            teleportInfo.sceneLoaderAgent.ExecuteLoadScene();
            if (teleportInfo.playerPositionTeleporter != null)
            {
                teleportInfo.playerPositionTeleporter.TeleportPlayer();
            }
        }

        private bool CheckQuestConditions(TeleportInfo teleportInfo)
        {
            for (int i = 0; i < teleportInfo.questConditions.Length; i++)
            {
                QuestCondition questCondition = teleportInfo.questConditions[i];
                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);
                if (quest == null || quest.state != questCondition.questState || quest.currentStepIndex != questCondition.questStepIndex)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckDefaultQuestConditions()
        {
            // Jika tidak ada kondisi, langsung anggap terpenuhi
            if (defaultQuestConditions == null || defaultQuestConditions.Length == 0)
            {
                return true;
            }

            // Pastikan QuestManager ada sebelum digunakan
            if (QuestManager.Instance == null)
            {
                Debug.LogError("QuestManager.Instance tidak ditemukan! Pastikan QuestManager ada di scene.");
                return false;
            }

            // Cek setiap kondisi quest
            for (int i = 0; i < defaultQuestConditions.Length; i++)
            {
                QuestCondition questCondition = defaultQuestConditions[i];

                // Cek apakah questInfo valid
                if (questCondition.questInfo == null)
                {
                    Debug.LogError($"QuestCondition pada index {i} tidak memiliki questInfo yang valid.");
                    return false;
                }

                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);

                // Cek apakah quest ditemukan
                if (quest == null)
                {
                    Debug.LogWarning($"Quest dengan ID {questCondition.questInfo.id} tidak ditemukan.");
                    return false;
                }

                // Cek apakah state dan step sesuai
                if (quest.state != questCondition.questState || quest.currentStepIndex != questCondition.questStepIndex)
                {
                    return false;
                }
            }

            return true; // Semua kondisi terpenuhi
        }

        private void ShowWarningPanel()
        {
            if (warningPanel != null)
            {
                warningPanel.SetActive(true);
            }
        }
    }

    [System.Serializable]
    public struct TeleportInfo
    {
        public QuestCondition[] questConditions;
        public SceneLoaderAgent sceneLoaderAgent;
        public PlayerPositionTeleporter playerPositionTeleporter;
    }

    [System.Serializable]
    public struct QuestCondition
    {
        public QuestInfoSO questInfo;
        public QuestState questState;
        public int questStepIndex;
    }
}

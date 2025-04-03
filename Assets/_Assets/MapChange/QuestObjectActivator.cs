using UnityEngine;
using DonBosco.Quests;
using DonBosco.SaveSystem;

public class QuestObjectActivator : MonoBehaviour
{
    public string questId; // ID quest yang ingin dicek
    public GameObject[] objectsToActivate; // GameObject yang akan diaktifkan

    void Start()
    {
        if (QuestManager.Instance != null)
        {
            ActivateObjects();
        }
        else
        {
            Debug.LogError("QuestManager tidak ditemukan!");
        }
    }

    void ActivateObjects()
    {
        Quest quest = QuestManager.Instance.GetQuestById(questId);
        if (quest != null && quest.state == QuestState.Active)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (SaveManager.Instance != null)
                {
                    bool isDisabled = SaveManager.Instance.IsObjectDisabled(obj.name);
                    Debug.Log($"Object {obj.name} should remain disabled: {isDisabled}");

                    if (isDisabled)
                    {
                        obj.SetActive(false); // Jangan aktifkan jika statusnya di save sebagai nonaktif
                        continue;
                    }
                }

                obj.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Quest tidak aktif atau tidak ditemukan: " + questId);
        }
    }

}
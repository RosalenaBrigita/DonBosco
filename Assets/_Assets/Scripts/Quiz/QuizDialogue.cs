using System.Collections;
using UnityEngine;
using DonBosco.Dialogue;
using UnityEngine.Events;
using UnityEngine.Networking;
using DonBosco.UI;

public class QuizDialogue : MonoBehaviour
{
    public bool IsInteractable { get; set; } = true;
    [SerializeField] private TextAsset quizDialogue; // Fallback jika offline
    [SerializeField] private int quizId;
    [SerializeField] private int npcId; // ID NPC untuk ambil dari database

    public UnityEvent OnQuizDone;
    [SerializeField] private bool hideUIScreenOnDone = false;

    private string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";
    private bool dialogueLoaded = false;

    public void StartDialogue()
    {
        Debug.Log("<color=orange>[Quiz] StartDialogue called!</color>");

        // Force activate jika diperlukan
        gameObject.SetActive(true);

        if (!dialogueLoaded)
        {
            StartCoroutine(LoadDialogueFromServer());
        }
        else
        {
            CheckHasAnswered();
        }
    }

    private IEnumerator LoadDialogueFromServer()
    {
        string url = $"{dialogueUrl}?npc_id={npcId}&quiz_id={quizId}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Using fallback dialogue: " + www.error);
            CheckHasAnswered(); // Gunakan dialog offline
        }
        else
        {
            QuizDialogueResponse response = JsonUtility.FromJson<QuizDialogueResponse>(www.downloadHandler.text);
            if (!string.IsNullOrEmpty(response.ink_json))
            {
                quizDialogue = new TextAsset(response.ink_json);
                dialogueLoaded = true;
                CheckHasAnswered();
            }
        }
    }

    [System.Serializable]
    private class QuizDialogueResponse
    {
        public string ink_json;
    }

    private void CheckHasAnswered()
    {
        Debug.Log("1. Masuk CheckHasAnswered");

        if (QuizManager.Instance == null)
        {
            Debug.LogError("QuizManager Instance NULL!");
            return;
        }

        if (!QuizManager.Instance.CheckHasAnswered(quizId))
        {
            Debug.Log("2. Quiz belum dijawab");

            // Cek DialogueManager
            var dm = DialogueManager.GetInstance();
            if (dm == null)
            {
                Debug.LogError("DialogueManager NULL!");
                return;
            }

            // Cek quizDialogue
            if (quizDialogue == null)
            {
                Debug.LogError("quizDialogue NULL!");
                return;
            }

            Debug.Log("3. Binding function Quiz...");
            dm.BindExternalFunction("Quiz", (answer) =>
            {
                Debug.Log($"4. Fungsi Quiz dipanggil: {answer}");
                SaveAnswer(answer);
            });

            Debug.Log("5. Memulai dialog...");
            dm.EnterDialogueMode(quizDialogue).OnDialogueDone((variable) =>
            {
                Debug.Log("6. Quiz selesai!");
                OnQuizDone.Invoke();
                if (hideUIScreenOnDone) UIManager.Instance.HideScreenUI();
            });
        }
        else
        {
            Debug.LogWarning("Quiz sudah dijawab sebelumnya");
        }
    }

    private void SaveAnswer(string answer)
    {
        string[] answerSplit = answer.Split('_');
        int quizId = int.Parse(answerSplit[0]);
        int quizAnswer = int.Parse(answerSplit[1]);
        QuizManager.Instance.SaveAnswer(quizId, quizAnswer);
    }
}
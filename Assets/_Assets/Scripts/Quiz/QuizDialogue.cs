using System.Collections;
using UnityEngine;
using DonBosco.Dialogue;
using UnityEngine.Events;
using UnityEngine.Networking;
using DonBosco.UI;

public class QuizDialogue : MonoBehaviour
{
    public bool IsInteractable { get; set; } = true;
    [SerializeField] private TextAsset quizDialogue; 
    [SerializeField] private int quizId;
    [SerializeField] private int npcId;

    public UnityEvent OnQuizDone;
    [SerializeField] private bool hideUIScreenOnDone = false;

    private string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";
    private bool dialogueLoaded = false;

    private void Start()
    {
        if (npcId > 0 && quizId > 0)
        {
            StartCoroutine(PreloadDialogueFromServer());
        }
        else
        {
            dialogueLoaded = true;
        }
    }

    public void StartDialogue()
    {
        Debug.Log("<color=orange>[Quiz] StartDialogue called!</color>");
        gameObject.SetActive(true);

        if (!dialogueLoaded)
        {
            Debug.LogWarning("[Quiz] Dialogue belum siap, masih loading...");
            return;
        }
        else
        {
            CheckHasAnswered();
        }
    }

    private IEnumerator PreloadDialogueFromServer()
    {
        string url = $"{dialogueUrl}?npc_id={npcId}&quiz_id={quizId}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            QuizDialogueResponse response = JsonUtility.FromJson<QuizDialogueResponse>(www.downloadHandler.text);
            if (!string.IsNullOrEmpty(response.ink_json))
            {
                quizDialogue = new TextAsset(response.ink_json);
                Debug.Log($"[Preload] Quiz dialogue loaded untuk NPC {npcId}, Quiz {quizId}");
            }
            else
            {
                Debug.LogWarning("[Preload] JSON kosong, pakai fallback");
            }
        }
        else
        {
            Debug.LogWarning($"[Preload] Gagal load: {www.error}, pakai fallback");
        }

        dialogueLoaded = true;
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

    [System.Serializable]
    private class QuizDialogueResponse
    {
        public string ink_json;
    }
}
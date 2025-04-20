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
            Debug.LogWarning("Using fallback dialogue: " + www.error);
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
        if (!QuizManager.Instance.CheckHasAnswered(quizId))
        {
            DialogueManager.GetInstance().BindExternalFunction("Quiz", (answer) => SaveAnswer(answer));
            DialogueManager.GetInstance().EnterDialogueMode(quizDialogue).OnDialogueDone((variable) =>
            {
                OnQuizDone.Invoke();
                if (hideUIScreenOnDone) UIManager.Instance.HideScreenUI();
            });
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
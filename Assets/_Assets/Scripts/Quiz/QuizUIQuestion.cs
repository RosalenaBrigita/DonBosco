using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUIQuestion : MonoBehaviour
{
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;


    public void SetQuestion(QuizSO quizSO, int playerAnswer)
    {
        leftText.text = quizSO.question;

        if (playerAnswer == 0)
        {
            // Belum dijawab
            leftText.text += "\n<color=#888888>(Belum dijawab)</color>";
            rightText.text = "<color=#888888>0";
        }
        else if (playerAnswer == quizSO.correctAnswer)
        {
            leftText.text += $"\n<color=#20841F>{quizSO.answers[playerAnswer - 1]}</color>";
            rightText.text = $"<color=#20841F>+{quizSO.points}";
        }
        else
        {
            leftText.text += $"\n<color=#841F2C>{quizSO.answers[playerAnswer - 1]}</color>";
            rightText.text = "<color=#841F2C>0";
        }
    }

}

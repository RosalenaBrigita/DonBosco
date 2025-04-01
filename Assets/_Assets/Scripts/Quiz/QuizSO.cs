using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuizType
{
    PointBased,
    DiplomacyMoral
}

[CreateAssetMenu(fileName = "QuizSO", menuName = "ScriptableObjects/QuizSO")]
public class QuizSO : ScriptableObject
{
    public string question;
    public string[] answers;
    public int correctAnswer;
    public int points;
    public QuizType quizType;  // Tambahkan tipe kuis
    public int moralEffect; // Tambahkan efek moral jika quizType == DiplomacyMoral
}

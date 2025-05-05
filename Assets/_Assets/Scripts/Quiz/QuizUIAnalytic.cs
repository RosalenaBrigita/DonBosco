using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DonBosco.Dialogue;

public class QuizUIAnalytic : MonoBehaviour
{
    [SerializeField] private QuizUIQuestion answerPrefab;
    [SerializeField] private Transform answerParent;

    [Header("UI")]
    [SerializeField] private TMP_Text quizScoreText;
    [SerializeField] private TMP_Text hasilPenyeranganText;
    [SerializeField] private TMP_Text hasilDiplomasiText;
    [SerializeField] private TMP_Text endingProgressText;

    void OnEnable()
    {
        ShowQuizAnalytic();
        ShowEndingResult();
    }

    private void ShowQuizAnalytic()
    {
        int quizScore = 0;
        int totalScore = 0;
        for (int i = 0; i < QuizManager.Instance.QuizSOs.Length; i++)
        {
            QuizSO quizSO = QuizManager.Instance.QuizSOs[i];

            if (quizSO.quizType != QuizType.PointBased)
                continue;

            int playerAnswer = QuizManager.Instance.QuizAnswers[i];
            QuizUIQuestion quizUIQuestion = Instantiate(answerPrefab, answerParent);
            quizUIQuestion.SetQuestion(quizSO, playerAnswer);

            if (playerAnswer == quizSO.correctAnswer)
            {
                quizScore += quizSO.points;
            }
            totalScore += quizSO.points;
        }
        quizScoreText.text = $"{quizScore}/{totalScore}";
    }

    private void ShowEndingResult()
    {
        var dialogueManager = DialogueManager.GetInstance();

        // Penyerangan
        string hasilPenyerangan = "Tidak diketahui";
        int penyeranganCode = 0;

        if (dialogueManager.GetVariableState("questBelakang") is Ink.Runtime.BoolValue belakang && belakang.value)
        {
            hasilPenyerangan = "Pahlawan Taktik";
            penyeranganCode = 1;
        }
        else if (dialogueManager.GetVariableState("questDepan") is Ink.Runtime.BoolValue depan && depan.value)
        {
            hasilPenyerangan = "Pahlawan Keberanian";
            penyeranganCode = 2;
        }
        else if (dialogueManager.GetVariableState("tungguPagi") is Ink.Runtime.BoolValue pagi && pagi.value)
        {
            hasilPenyerangan = "Gagal total";
            penyeranganCode = 3;
        }

        hasilPenyeranganText.text = hasilPenyerangan;

        // Diplomasi
        string hasilDiplomasi = "Tidak diketahui";
        int diplomasiCode = 0;

        if (dialogueManager.GetVariableState("sukses_total") is Ink.Runtime.BoolValue total && total.value)
        {
            hasilDiplomasi = "Berhasil Total";
            diplomasiCode = 1;
        }
        else if ((dialogueManager.GetVariableState("sukses_sebagian") is Ink.Runtime.BoolValue sebagian && sebagian.value) ||
                 (dialogueManager.GetVariableState("ending_terpaksa") is Ink.Runtime.BoolValue terpaksa && terpaksa.value))
        {
            hasilDiplomasi = "Berhasil Sebagian";
            diplomasiCode = 2;
        }
        else if (dialogueManager.GetVariableState("ending_gagal") is Ink.Runtime.BoolValue gagal && gagal.value)
        {
            hasilDiplomasi = "Gagal Total";
            diplomasiCode = 3;
        }

        hasilDiplomasiText.text = hasilDiplomasi;

        // Ending Progress Display (misal Ending X dari 9)
        int endingIndex = (penyeranganCode - 1) * 3 + diplomasiCode;
        if (penyeranganCode > 0 && diplomasiCode > 0)
        {
            endingProgressText.text = $"# {endingIndex} dari 9";
        }
        else
        {
            endingProgressText.text = "Belum lengkap";
        }
    }
}

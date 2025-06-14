using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DonBosco.Quests;

namespace DonBosco
{
    public class TargetManager : MonoBehaviour
    {
        public TargetController[] targets;
        public int maxActivations = 10;

        public TextMeshProUGUI scoreText;
        public GameObject finalPanel;
        public TextMeshProUGUI finalScoreText;
        public GameObject infoPanel;

        [Header("Quest Integration")]
        public QuestStepInfoSO questStepToAdvance;

        private int totalActivated = 0;
        private float spawnDelay = 2f;
        private float minDelay = 0.5f;

        private int score = 0;
        private int finishedTargets = 0;

        void Start()
        {
            InputManager.Instance.SetMovementActionMap(false);
            InputManager.Instance.SetUIActionMap(false);
            infoPanel.SetActive(true);

            if (finalPanel != null)
                finalPanel.SetActive(false);

            // Inisialisasi semua target
            foreach (var target in targets)
            {
                target.gameObject.SetActive(false);
            }
        }

        public void CloseInfoPanel()
        {
            infoPanel.SetActive(false);
            Time.timeScale = 1f; // Resume game
            StartCoroutine(SpawnRoutine()); // Mulai spawn setelah resume
            UpdateScoreUI();
        }

        public void OpenInfoPanel()
        {
            infoPanel.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }

        IEnumerator SpawnRoutine()
        {
            while (totalActivated < maxActivations)
            {
                // Cari target yang TIDAK aktif dan TIDAK aktif di hierarchy
                List<TargetController> availableTargets = new List<TargetController>();
                foreach (var target in targets)
                {
                    if (!target.IsActive && !target.gameObject.activeInHierarchy)
                        availableTargets.Add(target);
                }

                if (availableTargets.Count > 0)
                {
                    var randomTarget = availableTargets[Random.Range(0, availableTargets.Count)];
                    randomTarget.gameObject.SetActive(true);
                    randomTarget.ActivateTarget(OnTargetResult);
                    totalActivated++;
                }

                spawnDelay = Mathf.Max(minDelay, spawnDelay - 0.3f); // Kurangi delay lebih halus
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        void OnTargetResult(bool success)
        {
            if (success) score += 10;
            UpdateScoreUI();

            finishedTargets++;
            if (finishedTargets >= maxActivations)
            {
                EndGame();
            }
        }

        void ActivateRandomTarget()
        {
            List<TargetController> inactiveTargets = new List<TargetController>();
            foreach (var target in targets)
            {
                if (!target.IsActive)
                    inactiveTargets.Add(target);
            }

            if (inactiveTargets.Count == 0) return;

            var randomTarget = inactiveTargets[Random.Range(0, inactiveTargets.Count)];
            randomTarget.gameObject.SetActive(true);
            randomTarget.ActivateTarget(OnTargetResult);
        }

        void UpdateScoreUI()
        {
            if (scoreText != null)
                scoreText.text = "Score : " + score.ToString();
        }

        void EndGame()
        {
            if (scoreText != null)
                scoreText.gameObject.SetActive(false);

            if (finalPanel != null)
            {
                finalPanel.SetActive(true);
                if (finalScoreText != null)
                    finalScoreText.text = "Skor Akhir: " + score.ToString();
            }

            if (questStepToAdvance != null)
            {
                GameEventsManager.Instance.questEvents.AdvanceQuest(questStepToAdvance.questInfo.id);
                Debug.Log("Quest advanced: " + questStepToAdvance.questInfo.id);
            }
        }

        public void ChangeMapInput()
        {
            // **Kembalikan ke input gerak**
            InputManager.Instance.SetMovementActionMap(true);
            InputManager.Instance.SetUIActionMap(true);
        }
    }

}

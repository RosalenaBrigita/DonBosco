using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SewingMinigame : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineRenderer guideLineRenderer;
    public Slider progressBar;
    public Image progressFill;

    public Image guide1;
    public Image guide2;
    public Image guide3;

    public RectTransform sewingPanel;
    public GameObject finalPanel;
    public GameObject infoPanel; // Panel informasi

    public float maxOffset = 0.2f;
    private int stage = 1;

    private List<Vector3> drawnPoints = new List<Vector3>();
    private List<Vector3> sewingPathPoints = new List<Vector3>();
    private bool isSewing = false;
    private bool isInfoPanelActive = true; // Awalnya aktif karena panel info muncul dulu

    private GameObject logicCamera;

    void Start()
    {
        SetupLineRenderer();
        GenerateSewingPath();
        UpdateGuideVisibility();
        finalPanel.SetActive(false); // Pastikan final panel tidak aktif di awal
        infoPanel.SetActive(true); // Info panel aktif di awal

        logicCamera = GameObject.Find("Main Camera"); // Simpan referensi kamera
        if (logicCamera != null)
        {
            logicCamera.SetActive(false);
        }

        // **Matikan input gerak, aktifkan input UI**
        DonBosco.InputManager.Instance.SetMovementActionMap(false);
        DonBosco.InputManager.Instance.SetUIActionMap(true);
    }

    void SetupLineRenderer()
    {
        lineRenderer.sortingLayerName = "UI";
        lineRenderer.sortingOrder = 5;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;

        guideLineRenderer.useWorldSpace = true;
        guideLineRenderer.startWidth = 0.02f;
        guideLineRenderer.endWidth = 0.02f;
        guideLineRenderer.startColor = Color.green;
        guideLineRenderer.endColor = Color.green;

        progressBar.value = 0;
    }

    void GenerateSewingPath()
    {
        sewingPathPoints.Clear();
        RectTransform benderaRect = GetComponent<RectTransform>();

        float margin = 60f;
        Vector3 leftSide = benderaRect.TransformPoint(new Vector3(-benderaRect.rect.width / 2 + margin, 0, 0));
        Vector3 rightSide = benderaRect.TransformPoint(new Vector3(benderaRect.rect.width / 2 - margin, 0, 0));
        float middleY = benderaRect.TransformPoint(new Vector3(0, 0, 0)).y;

        for (float x = leftSide.x; x <= rightSide.x; x += 0.1f)
        {
            float yOffset = 0f;
            if (stage == 2)
            {
                yOffset = Mathf.Sin(x * 2) * 0.2f;
            }
            else if (stage == 3)
            {
                yOffset = Mathf.Sin(x * 4.5f) * 0.29f;
            }

            sewingPathPoints.Add(new Vector3(x, middleY + yOffset, 0f));
        }

        guideLineRenderer.positionCount = sewingPathPoints.Count;
        guideLineRenderer.SetPositions(sewingPathPoints.ToArray());
    }

    void UpdateGuideVisibility()
    {
        guide1.gameObject.SetActive(stage == 1);
        guide2.gameObject.SetActive(stage == 2);
        guide3.gameObject.SetActive(stage == 3);
    }

    void Update()
    {
        // Cek apakah infoPanel masih aktif
        isInfoPanelActive = infoPanel.activeSelf;

        // Jika info panel masih aktif, abaikan input
        if (isInfoPanelActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartSewing();
        }
        else if (Input.GetMouseButton(0) && isSewing)
        {
            ContinueSewing();
            EvaluateSewingRealtime();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FinishSewing();
        }
    }

    void StartSewing()
    {
        isSewing = true;
        drawnPoints.Clear();
        lineRenderer.positionCount = 0;
    }

    void ContinueSewing()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (IsWithinPanel(mousePos)) // Hanya tambahkan titik jika dalam sewingPanel
        {
            drawnPoints.Add(mousePos);
            lineRenderer.positionCount = drawnPoints.Count;
            lineRenderer.SetPositions(drawnPoints.ToArray());
        }
    }

    void FinishSewing()
    {
        isSewing = false;

        if (drawnPoints.Count == 0) return; // Cegah evaluasi jika tidak ada titik jahitan

        EvaluateSewingFinal();
        drawnPoints.Clear();
        lineRenderer.positionCount = 0;
    }

    bool IsWithinPanel(Vector3 worldPosition)
    {
        Vector3 localPos = sewingPanel.InverseTransformPoint(worldPosition);
        return sewingPanel.rect.Contains(localPos);
    }

    void EvaluateSewingRealtime()
    {
        if (drawnPoints.Count == 0) return; // Jangan update kalau belum ada titik

        int validPoints = 0;
        int totalGuidePoints = sewingPathPoints.Count;

        foreach (Vector3 guidePoint in sewingPathPoints)
        {
            foreach (Vector3 drawnPoint in drawnPoints)
            {
                if (IsWithinPanel(drawnPoint) && Vector3.Distance(drawnPoint, guidePoint) < maxOffset)
                {
                    validPoints++;
                    break;
                }
            }
        }

        if (validPoints == 0) return; // Jangan update progress kalau semua titik tidak valid

        float currentScore = (float)validPoints / totalGuidePoints;
        progressBar.value = currentScore;
        UpdateProgressColor(currentScore);
    }

    void EvaluateSewingFinal()
    {
        float finalScore = progressBar.value;
        UpdateProgressUI(finalScore);
    }

    void UpdateProgressColor(float score)
    {
        if (score >= 0.9f)
        {
            progressFill.color = Color.green;
        }
        else if (score >= 0.75f)
        {
            progressFill.color = Color.yellow;
        }
        else
        {
            progressFill.color = Color.red;
        }
    }

    void UpdateProgressUI(float finalScore)
    {
        string result = "";

        if (finalScore >= 0.9f)
        {
            result = "Perfect";
            NextStage();
        }
        else if (finalScore >= 0.75f)
        {
            result = "Good";
            NextStage();
        }
        else
        {
            result = "Bad";
            RestartStage();
        }

        GetComponent<FinalScoreDisplay>().ShowFinalScore(result);
    }

    void NextStage()
    {
        if (stage < 3)
        {
            stage++;
            GenerateSewingPath();
            UpdateGuideVisibility();
        }
        else
        {
            Debug.Log("Semua jahitan selesai! Bendera berhasil dibuat!");
            Destroy(guide3.gameObject);
            StartCoroutine(ShowFinalPanelAfterDelay());
        }
    }

    void RestartStage()
    {
        drawnPoints.Clear();
        lineRenderer.positionCount = 0;
        progressBar.value = 0;
    }

    IEnumerator ShowFinalPanelAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Tunggu animasi progress selesai
        finalPanel.SetActive(true);
    }

    // Fungsi untuk menyembunyikan info panel dan mengizinkan sewing
    public void HideInfo()
    {
        infoPanel.SetActive(false);
        isInfoPanelActive = false;
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        isInfoPanelActive = true;
    }

    public void ChangeMapInput()
    {
        // **Kembalikan ke input gerak**
        DonBosco.InputManager.Instance.SetMovementActionMap(true);
        if (logicCamera != null)
        {
            logicCamera.SetActive(true);
        }
    }
}
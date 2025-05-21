using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DonBosco;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button buttonInfo;

    private void OnEnable()
    {
        GameManager.OnGameModeChange += HandleGameModeChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameModeChange -= HandleGameModeChange;
    }

    private void Start()
    {
        // Cek mode saat awal hidup
        HandleGameModeChange(GameManager.GameMode);
    }

    private void HandleGameModeChange(GameMode mode)
    {
        Debug.Log("GameMode changed to: " + mode);
        
        if (mode == GameMode.Explore)
        {
            buttonInfo.gameObject.SetActive(true);
        }
        else
        {
            buttonInfo.gameObject.SetActive(false);
            Debug.Log("IYA");
        }
    }

    public void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            DonBosco.InputManager.Instance.SetMovementActionMap(false);
        }
    }

    public void HideTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
            DonBosco.InputManager.Instance.SetMovementActionMap(true);
        }
    }
}

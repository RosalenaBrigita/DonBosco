using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DonBosco.SaveSystem;
using System.Threading.Tasks;

namespace DonBosco
{
    public class MapManager : MonoBehaviour, ISaveLoad
    {
        [Header("UI Map")]
        [SerializeField] private GameObject Map;                 // Panel map
        [SerializeField] private Button Info;                    // Tombol info
        [SerializeField] private RectTransform playerIcon;       // Ikon player di map
        [SerializeField] private Button Tutorial;  

        [Header("Referensi Dunia Nyata")]
        [SerializeField] private Transform player;               // Transform player

        [Header("Koordinat Dunia (Area Map)")]
        [SerializeField] private Vector2 worldBottomLeft = new Vector2(-71f, -78f);
        [SerializeField] private Vector2 worldTopRight = new Vector2(56.9f, 4f);

        [Header("Target Posisi di UI Map")]
        [SerializeField] private Vector2 uiBottomLeft = new Vector2(-934f, -509f);
        [SerializeField] private Vector2 uiTopRight = new Vector2(934f, 509f);

        [Header("Scene yang Diizinkan")]
        [SerializeField] private string[] allowedScenes;

        private Vector2 scale;
        private Vector2 offset;
        private bool isAllowedScene = false;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SaveManager.Instance.Subscribe(this);
            CheckScene(); // Cek saat pertama aktif
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SaveManager.Instance.Unsubscribe(this);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CheckScene();
        }

        private void CheckScene()
        {
            isAllowedScene = false;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (Array.Exists(allowedScenes, s => s.Equals(scene.name, StringComparison.OrdinalIgnoreCase)))
                {
                    isAllowedScene = true;
                    break;
                }
            }
        }

        private void Start()
        {
            // Hitung skala dan offset dari world ke UI
            scale = new Vector2(
                (uiTopRight.x - uiBottomLeft.x) / (worldTopRight.x - worldBottomLeft.x),
                (uiTopRight.y - uiBottomLeft.y) / (worldTopRight.y - worldBottomLeft.y)
            );

            offset = new Vector2(
                uiBottomLeft.x - worldBottomLeft.x * scale.x,
                uiBottomLeft.y - worldBottomLeft.y * scale.y
            );
        }

        private void Update()
        {
            if (!isAllowedScene) return;
            UpdatePlayerIconPosition(); 
        }

        private void HideInfo()
        {
            Info.gameObject.SetActive(false);
            Tutorial.gameObject.SetActive(false);
        }

        private void ShowInfo()
        {
            Info.gameObject.SetActive(true);
            Tutorial.gameObject.SetActive(true);
        }

        public void HideMapDetail()
        {
            Map.SetActive(false);
            ShowInfo();
            InputManager.Instance.SetMovementActionMap(true);
        }

        public void ShowMapDetail()
        {
            HideInfo();
            Map.SetActive(true);
            InputManager.Instance.SetMovementActionMap(false);
        }

        private void UpdatePlayerIconPosition()
        {
            Vector2 worldPos = new Vector2(player.position.x, player.position.y);

            Vector2 uiPos = new Vector2(
                worldPos.x * scale.x + offset.x,
                worldPos.y * scale.y + offset.y
            );
            uiPos.x += -35f;
            uiPos.y += -100f;
            playerIcon.anchoredPosition = uiPos;
        }

        #region SaveLoad
        public async Task Save(SaveData saveData)
        {
            saveData.playerIconPosition = playerIcon.anchoredPosition;
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if(saveData == null)
                return;
            
            playerIcon.anchoredPosition = saveData.playerIconPosition;
            await Task.CompletedTask;
        }
        #endregion
    }
}

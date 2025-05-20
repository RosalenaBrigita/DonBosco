using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class MapManager : MonoBehaviour
    {
        [Header("UI Map")]
        [SerializeField] private GameObject Map;                 // Panel map
        [SerializeField] private Button Info;                    // Tombol info
        [SerializeField] private RectTransform playerIcon;       // Ikon player di map

        [Header("Referensi Dunia Nyata")]
        [SerializeField] private Transform player;               // Transform player

        [Header("Koordinat Dunia (Area Map)")]
        [SerializeField] private Vector2 worldBottomLeft = new Vector2(-71f, -78f);
        [SerializeField] private Vector2 worldTopRight = new Vector2(56.9f, 4f);

        [Header("Target Posisi di UI Map")]
        [SerializeField] private Vector2 uiBottomLeft = new Vector2(-934f, -509f);
        [SerializeField] private Vector2 uiTopRight = new Vector2(934f, 509f);

        private Vector2 scale;
        private Vector2 offset;

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
            if (Map.activeSelf)
            {
                UpdatePlayerIconPosition();
            }
        }

        private void HideInfo()
        {
            Info.gameObject.SetActive(false);
        }

        private void ShowInfo()
        {
            Info.gameObject.SetActive(true);
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

            // Mapping world ke UI menggunakan skala dan offset
            Vector2 uiPos = new Vector2(
                worldPos.x * scale.x + offset.x,
                worldPos.y * scale.y + offset.y
            );
            uiPos.x += -35f;
            uiPos.y += -100f;
            playerIcon.anchoredPosition = uiPos;
        }
    }
}

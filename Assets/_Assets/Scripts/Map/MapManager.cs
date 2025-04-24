using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private GameObject Map;
        [SerializeField] private Button Info;

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
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.UI
{
    public class UIAddToStack : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject backToMenuConfirmation;
        [SerializeField] private GameObject backToMenuNotExploreMode;

        public void AddToStack()
        {
            // Cek apakah GameMode adalah "Explore"
            if (GameManager.GameMode == GameMode.Explore)
            {
                if (backToMenuConfirmation != null)
                {
                    UIManager.Instance.PushUI(backToMenuConfirmation);
                }
            }
            else
            {
                if (backToMenuNotExploreMode != null)
                {
                    UIManager.Instance.PushUI(backToMenuNotExploreMode);
                }
            }
        }

        public void RemoveFromStack()
        {
            UIManager.Instance.PopUI();
        }

        public void ClearStack()
        {
            UIManager.Instance.ClearUIStack();
        }
    }
}

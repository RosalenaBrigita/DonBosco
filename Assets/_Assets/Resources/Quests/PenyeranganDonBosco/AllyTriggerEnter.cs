using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class AllyTriggerEnter : MonoBehaviour
    {
        [Header("Ally Trigger Settings")]
        [SerializeField] private bool disableInputMoveOnEnter = true;

        [Tooltip("Event yang dipanggil saat layer 'Ally' masuk trigger.")]
        [SerializeField] private UnityEvent onAllyEnter = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Cek apakah yang masuk adalah objek dengan layer "Ally"
            if (other.gameObject.layer == LayerMask.NameToLayer("Ally"))
            {
                // Optional: Disable movement lewat InputManager (jika ada)
                if (disableInputMoveOnEnter && InputManager.Instance != null)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }

                // Jalankan event jika ada
                onAllyEnter?.Invoke();
            }
        }
    }
}

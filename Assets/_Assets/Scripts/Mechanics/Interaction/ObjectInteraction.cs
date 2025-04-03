using System.Collections;
using System.Collections.Generic;
using DonBosco.ItemSystem;
using UnityEngine;
using UnityEngine.Events;
using DonBosco.SaveSystem;

namespace DonBosco
{
    public class ObjectInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onInteract = null;
        [SerializeField] private bool disableInputMoveOnEnter = false;
        [SerializeField] private string objectID;
        [SerializeField] private bool isDestroyable = false; // Tambahkan flag untuk menentukan apakah objek bisa dihancurkan

        public bool IsInteractable { get; set; } = true;

        private void Start()
        {
            // Jika objek bisa dihancurkan dan sudah dinonaktifkan sebelumnya, jangan munculkan kembali
            if (isDestroyable && SaveManager.Instance != null && SaveManager.Instance.IsObjectDisabled(objectID))
            {
                gameObject.SetActive(false);
            }
        }

        public void Interact()
        {
            if (IsInteractable)
            {
                if (disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }

                // Jika objek bisa dihancurkan, simpan statusnya di sistem penyimpanan
                if (isDestroyable && !string.IsNullOrEmpty(objectID))
                {
                    SaveManager.Instance.SetObjectDisabled(objectID, true);
                }

                onInteract?.Invoke();
            }
        }

        public void Interact(Item item)
        {
            if (IsInteractable)
            {
                if (disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }
                onInteract?.Invoke();
            }
        }
    }
}
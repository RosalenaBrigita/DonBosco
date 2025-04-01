using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

namespace DonBosco
{
    /// <summary>
    /// Handles input
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        private InputActionMap movementActionMap;
        private InputActionMap UIactionMap;
        private PlayerInput playerInput;

        private Vector2 moveValue = Vector2.zero;
        private bool aimPressed = false;
        private bool interactPressed = false;
        private bool pausePressed = false;
        private bool attackPressed = false;
        private bool pickupPressed = false;
        private bool dropPressed = false;
        private bool inventoryPressed = false; // Tambahan untuk Inventory
        private int numkeysPressed = 0;
        private bool useItemPressed = false;
        private float scrollWheelValue = 0f;
        private bool sprintPressed = false;
        private bool dashPressed = false;

        private bool submitPressed = false;
        private bool backPressed = false;
        private bool leftClickPressed = false;

        private static InputManager instance;
        public static InputManager Instance => instance;

        #region Events
        public event Action OnAimPressed;
        public event Action OnInteractPressed;
        public event Action OnPausePressed;
        public event Action OnAttackPressed;
        public event Action OnInventoryPressed; // Tambahan event Inventory
        public event Action<bool> OnSprintPressed; // Event untuk Sprint
        public event Action OnDashPressed;
        #endregion

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Input Manager in the scene.");
            }
            instance = this;

            playerInput = GetComponent<PlayerInput>();
            movementActionMap = playerInput.actions.FindActionMap("Movement");
            UIactionMap = playerInput.actions.FindActionMap("UI");
        }

        #region Input
        #region Movement
        public void OnMove(InputValue value)
        {
            moveValue = value.Get<Vector2>();
        }

        public void OnAim(InputValue value)
        {
            aimPressed = value.isPressed;
            OnAimPressed?.Invoke();
        }

        public void OnInteract(InputValue value)
        {
            interactPressed = value.isPressed;
            OnInteractPressed?.Invoke();
        }

        public void OnPause(InputValue value)
        {
            pausePressed = value.isPressed;
            OnPausePressed?.Invoke();
        }

        public void OnAttack(InputValue value)
        {
            if (value.isPressed)
            {
                OnAttackPressed?.Invoke(); // Hanya panggil saat tombol ditekan
            }
        }

        public void OnPickup(InputValue value)
        {
            pickupPressed = value.isPressed;
        }

        public void OnDrop(InputValue value)
        {
            dropPressed = value.isPressed;
        }

        public void OnNumKeys(InputValue value)
        {
            int.TryParse(value.Get().ToString(), out numkeysPressed);
        }

        public void OnScrollWheel(InputValue value)
        {
            Vector2 temp = value.Get<Vector2>();
            scrollWheelValue = temp.y;
        }

        public void OnUseItem(InputValue value)
        {
            useItemPressed = value.isPressed;
        }

        public void OnInventory(InputValue value) // Tambahan untuk Inventory
        {
            inventoryPressed = value.isPressed;
            OnInventoryPressed?.Invoke();
        }

        public void OnSprint(InputValue value)
        {
            sprintPressed = value.isPressed;
            OnSprintPressed?.Invoke(sprintPressed); // Pastikan nilai dikirim ke event
        }

        public void OnDash(InputValue value)
        {
            dashPressed = value.isPressed;
            OnDashPressed?.Invoke();
        }

        #endregion

        #region UI
        public void OnSubmit(InputValue value)
        {
            submitPressed = value.isPressed;
        }

        public void OnBack(InputValue value)
        {
            backPressed = value.isPressed;
        }

        public void OnLeftClick(InputValue value)
        {
            leftClickPressed = value.isPressed;
        }
        #endregion
        #endregion

        #region Getters
        public Vector2 GetMoveValue()
        {
            return moveValue;
        }

        public bool GetAttackValue()
        {
            return attackPressed;
        }

        public float GetScrollWheelValue()
        {
            return scrollWheelValue;
        }

        public bool GetUseItemPressed()
        {
            return useItemPressed;
        }

        public bool GetAimPressed()
        {
            bool temp = aimPressed;
            aimPressed = false;
            return temp;
        }

        public bool GetInteractPressed()
        {
            bool temp = interactPressed;
            interactPressed = false;
            return temp;
        }

        public bool GetPausePressed()
        {
            bool temp = pausePressed;
            pausePressed = false;
            return temp;
        }

        public bool GetPickupPressed()
        {
            bool temp = pickupPressed;
            pickupPressed = false;
            return temp;
        }

        public bool GetDropPressed()
        {
            bool temp = dropPressed;
            dropPressed = false;
            return temp;
        }

        public int GetNumKeysPressed()
        {
            int temp = numkeysPressed;
            numkeysPressed = 0;
            return temp;
        }

        public bool GetSubmitPressed()
        {
            bool temp = submitPressed;
            submitPressed = false;
            return temp;
        }

        public bool GetBackPressed()
        {
            bool temp = backPressed;
            backPressed = false;
            return temp;
        }

        public bool GetLeftClickPressed()
        {
            bool temp = leftClickPressed;
            leftClickPressed = false;
            return temp;
        }

        public bool GetInventoryPressed() // Getter untuk Inventory
        {
            bool temp = inventoryPressed;
            inventoryPressed = false;
            return temp;
        }
        public bool GetSprintValue()
        {
            return sprintPressed;
        }

        public bool GetDashPressed()
        {
            bool temp = dashPressed;
            dashPressed = false;
            return temp;
        }

        #endregion

        #region Register
        public bool RegisterSubmitPressed()
        {
            return submitPressed;
        }
        #endregion

        /// <summary>
        /// Set the movement action map
        /// </summary>
        public void SetMovementActionMap(bool value)
        {
            if (value)
            {
                movementActionMap.Enable();
            }
            else
            {
                movementActionMap.Disable();
            }
        }

        public void SetUIActionMap(bool value)
        {
            if (value)
            {
                UIactionMap.Enable();
            }
            else
            {
                UIactionMap.Disable();
            }
        }
    }
}

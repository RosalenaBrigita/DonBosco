using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles Player state and variables
    /// </summary>
    public class PlayerController : MonoBehaviour, ISaveLoad
    {
        private CharacterMovement playerMovement;
        private CharacterAttack playerAttack;
        private DrawLineRelativeMouse drawLine;

        private Vector2 input;
        private bool previousAimPressed = false;

        private MovementState movementState = MovementState.Walking;
        internal enum MovementState
        {
            Walking,
            Aiming
        }



        #region MonoBehaviour
        private void Awake() 
        {
            playerMovement = GetComponent<CharacterMovement>();
            playerAttack = GetComponent<CharacterAttack>();
            drawLine = GetComponentInChildren<DrawLineRelativeMouse>();
        }

        void OnEnable()
        {
            InputManager.Instance.OnAimPressed += OnAimPressed;
            GameEventsManager.Instance.playerEvents.onChangeScene += OnSceneChange;
            SaveManager.Instance.Subscribe(this);
            GameManager.OnEnterGameOver += OnEnterGameOver;
            GameManager.OnRetry += OnRetry;
        }

        void OnDisable()
        {
            InputManager.Instance.OnAimPressed -= OnAimPressed;
            GameEventsManager.Instance.playerEvents.onChangeScene -= OnSceneChange;
            SaveManager.Instance.Unsubscribe(this);
            GameManager.OnEnterGameOver -= OnEnterGameOver;
            GameManager.OnRetry -= OnRetry;
        }

        void Update()
        {
            if(InputManager.Instance.GetAttackValue())
            {
                if(movementState == MovementState.Aiming)
                {
                    playerAttack.Attack(drawLine.AngleToTarget, drawLine.StartingLineTransform.position);
                }
            }
        }

        private void FixedUpdate() {
            switch(movementState)
            {
                case MovementState.Walking:
                    playerMovement.WalkState();
                    break;
                case MovementState.Aiming:
                    playerMovement.AimState(drawLine.AimPosition);

                    //Change the sprite image to face the mouse position
                    Vector2 lookDirection = drawLine.AimPosition - transform.position;
                    float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                    //continue...
                    break;
            }
        }
        #endregion



        #region Input
        public void OnAimPressed()
        {
            bool value = InputManager.Instance.GetAimPressed();
            //Draw the aim line to the mouse position (Top priority)
            switch(value)
            {
                case true:
                    drawLine.DrawLine();

                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                case false:
                    drawLine.RemoveLine();

                    //Reset cursor to center
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }

            movementState = value ? MovementState.Aiming : MovementState.Walking;
            
            //Hide the cursor when pressed, show it when released
            Cursor.visible = !value;
            previousAimPressed = value;
        }

        

        private void OnSceneChange(string sceneName)
        {
            drawLine.RemoveLine();

            //Reset cursor to center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;

            movementState = MovementState.Walking;
            
            //Hide the cursor when pressed, show it when released
            Cursor.visible = true;
            previousAimPressed = false;
        }
        #endregion


        private void OnEnterGameOver()
        {
            drawLine.RemoveLine();

            //Reset cursor to center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;

            movementState = MovementState.Walking;
            
            //Hide the cursor when pressed, show it when released
            Cursor.visible = true;
            previousAimPressed = false;

            //Player death
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }

        private void OnRetry()
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }



        #region SaveLoad
        public async Task Save(SaveData saveData)
        {
            saveData.playerPosition = transform.position;
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if(saveData == null)
                return;
            
            transform.position = saveData.playerPosition;
            await Task.CompletedTask;
        }
        #endregion
    }
}

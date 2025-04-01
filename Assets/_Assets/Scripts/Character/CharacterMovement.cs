using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles character movement and animation
    /// </summary>
    public class CharacterMovement : MonoBehaviour 
    {
        [Header("References")]
        [SerializeField] private Transform visionTransform;
        [SerializeField] private Animator anim;
        [SerializeField] private CharacterAnimator characterAnimator;
        [Header("Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float aimWalkSpeed = 2f;
        [SerializeField] private float sprintSpeed = 10f; // Kecepatan saat lari
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 5f;

        private bool isSprinting = false; // Status apakah sedang berlari
        private bool isDashing = false;
        private bool canDash = true;

        private PlayerAimWeapon playerAimWeapon;

        private Rigidbody2D rb;

        private Vector2 movementDirection;
        private Vector2 facingDirection; 
        float facingAngle;
        private bool isAiming = false;
        private bool lockMovementAnim = false;
        private const string IDLE = "Idle";
        private const string SIDE = "side";
        private const string UP = "up";
        private const string DOWN = "down";
        string state = "";


        #region MonoBehaviour
        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            playerAimWeapon = GetComponent<PlayerAimWeapon>();
        }

        void OnEnable()
        {
            GetComponent<PlayerItem>().OnPickup += PickupAnim;
            InputManager.Instance.OnSprintPressed += OnSprintChanged;
            InputManager.Instance.OnDashPressed += Dash;
        }

        void OnDisable()
        {
            GetComponent<PlayerItem>().OnPickup -= PickupAnim;
            InputManager.Instance.OnSprintPressed -= OnSprintChanged;
            InputManager.Instance.OnDashPressed -= Dash;
        }

        void Update()
        {
            if (!isAiming && !lockMovementAnim)
            {
                AnimateMovement();
            }
        }

        void FixedUpdate() 
        {
            if (!isDashing) // Hanya bergerak jika tidak sedang dash
            {
                Move();
            }
        }
        #endregion


        #region Methods
        public void WalkState()
        {
            isAiming = false;
            visionTransform.GetComponent<FaceTowardGameObject>().startFacingTarget = false;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", false);
        }
        
        public void AimState(Vector2 aimPosition)
        {
            isAiming = true;
            visionTransform.GetComponent<FaceTowardGameObject>().startFacingTarget = true;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", true);

            //Rotate towards the target
            Vector2 direction = aimPosition - (Vector2)transform.position;
            AnimateFacingDirection(direction.normalized);
        }

        /// <summary>
        /// Rotates the vision transform to face the direction of the movement
        /// </summary>
        private void RotateVision()
        {
            if(movementDirection != Vector2.zero)
            {
                facingDirection = movementDirection;
                facingAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            }
            if(visionTransform != null)
            {
                visionTransform?.DOLocalRotate(new Vector3(0, 0, facingAngle), 0.1f);
            }
        }

        /// <summary>
        /// Called once by InputSystem OnMove event trigger
        /// </summary>
        public void Move()
        {
            movementDirection = InputManager.Instance.GetMoveValue().normalized;
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed; // Pilih speed

            if (isAiming)
            {
                rb.MovePosition(rb.position + movementDirection * aimWalkSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + movementDirection * currentSpeed * Time.fixedDeltaTime);

                //Rotate the vision transform when moving and not aiming
                RotateVision();
            }
        }

        /// <summary>
        /// Called once by InputSystem OnAim event trigger
        /// </summary>
        public void Aim(bool pressed)
        {
            anim.SetBool("Aiming", pressed);
        }

        private void AnimateMovement()
        {
            if (isDashing) return; // Jika sedang dash, animasi jalan tidak dimainkan

            anim.speed = isSprinting ? 2f : 1f; // Animasi lebih cepat saat lari
            bool isMoving = movementDirection.magnitude > 0.1f;
            Vector2 direction = movementDirection.normalized;

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (characterAnimator.currentState != state && isMoving)
            {
                characterAnimator.ChangeState(state);
            }
            else if(characterAnimator.currentState != state+IDLE && !isMoving)
            {
                characterAnimator.ChangeState(state+IDLE);
            }
        }

        public void AnimateFacingDirection(Vector2 direction)
        {
            anim.speed = 0.5f;
            bool isMoving = movementDirection.magnitude > 0.1f;

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(1, 1, 1);
            }
            
            if(!isMoving && !string.IsNullOrEmpty(state))
            {
                characterAnimator.ChangeState(state+IDLE);
                return;
            }
            characterAnimator.ChangeState(state);
        }

        private void PickupAnim()
        {
            string previousState = characterAnimator.currentState;
            characterAnimator.ChangeState("pickup");
            StartCoroutine(ResetState(previousState));
            lockMovementAnim = true;

            IEnumerator ResetState(string state)
            {
                yield return new WaitForSeconds(1f);
                characterAnimator.ChangeState(state);
                lockMovementAnim = false;
            }
        }

        private void OnSprintChanged(bool isPressed)
        {
            isSprinting = isPressed;
            Debug.Log("Sprint Status: " + isSprinting);
        }

        private void Dash()
        {
            Debug.Log("Dash function called");

            if (!isDashing && canDash && movementDirection.magnitude > 0.1f)
            {
                Debug.Log("Starting Dash Coroutine");
                StartCoroutine(DashCoroutine());
            }
            else
            {
                Debug.Log($"Dash not executed: isDashing={isDashing}, canDash={canDash}, movementDirection={movementDirection}");
            }
        }

        private IEnumerator DashCoroutine()
        {
            isDashing = true;
            canDash = false;

            anim.enabled = false; // Hentikan animasi selama dash
            Vector2 dashDirection = movementDirection.normalized;

            float startTime = Time.time;
            Debug.Log($"Dash Direction: {dashDirection}");

            while (Time.time < startTime + dashDuration)
            {
                rb.velocity = dashDirection * dashSpeed;
                Debug.Log($"Dashing... Velocity: {rb.velocity}");
                yield return null;
            }

            rb.velocity = Vector2.zero; // Setel velocity ke 0 setelah dash selesai
            Debug.Log("Dash ended");

            isDashing = false;
            anim.enabled = true; // Aktifkan animasi kembali

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            Debug.Log("Dash cooldown complete, can dash again");
        }
        #endregion
    }
}

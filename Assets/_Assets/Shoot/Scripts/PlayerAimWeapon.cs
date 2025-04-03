using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;
using DonBosco.Audio;
using UnityEngine.UI;

namespace DonBosco.Character
{
    public class PlayerAimWeapon : MonoBehaviour
    {
        [SerializeField] private Transform aimTransform;
        [SerializeField] private Animator aimAnimator;
        [SerializeField] private Transform aimGunEndPointTransform;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject uiBulletPrefab;
        [SerializeField] private Transform reloadBar;
        [SerializeField] private int maxAmmo = 6;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float reloadDelay = 0.5f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private bool weaponSoundEffect = true;

        [SerializeField] private string[] allowedScenes; // Scene yang diperbolehkan menembak
        private bool canShoot = false;

        public event EventHandler<ShootEventArgs> OnShoot;

        [Serializable]
        public class ShootEventArgs : EventArgs
        {
            public Vector3 gunEndPointPosition;
            public Vector3 shootPosition;
        }

        private int currentAmmo;
        private float bulletSpacing = 23.6f;
        private float startX = -60.9f; // Posisi X pertama peluru di UI
        private bool isReloading = false;

        private void Awake()
        {
            if (aimTransform == null) aimTransform = transform.Find("Aim");
            if (aimTransform == null) { Debug.LogError("Aim Transform tidak ditemukan!", this); return; }

            if (aimAnimator == null) aimAnimator = aimTransform.GetComponent<Animator>();
            if (aimAnimator == null) Debug.LogError("Animator tidak ditemukan!", this);

            if (aimGunEndPointTransform == null) aimGunEndPointTransform = aimTransform.Find("GunEndPointTransform");
            if (aimGunEndPointTransform == null) Debug.LogError("Gun End Point Transform tidak ditemukan!", this);

            currentAmmo = maxAmmo;
            LoadUIBullets();
        }

        private void Start()
        {
            //Debug.Log("Allowed Scenes: " + string.Join(", ", allowedScenes));
            CheckScene();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnAttackPressed += FireBullet;
            }
            else
            {
                Debug.LogError("InputManager.Instance adalah null! Pastikan InputManager ada dalam scene.");
            }
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnAttackPressed -= FireBullet;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("Scene changed to: " + scene.name);
            CheckScene();
        }

        private void CheckScene()
        {
            bool foundAllowedScene = false;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                //Debug.Log($"Checking loaded scene: {scene.name}");

                if (Array.Exists(allowedScenes, s => s.Equals(scene.name, StringComparison.OrdinalIgnoreCase)))
                {
                    foundAllowedScene = true;
                    break;
                }
            }

            canShoot = foundAllowedScene;
            //Debug.Log($"Final Can Shoot: {canShoot}");

            // Sembunyikan atau tampilkan weapon berdasarkan scene
            aimTransform.gameObject.SetActive(canShoot);

            // Atur event Input Attack
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnAttackPressed -= FireBullet;
                if (canShoot)
                {
                    InputManager.Instance.OnAttackPressed += FireBullet;
                }
            }
        }

        private void Update()
        {
            if (canShoot)
            {
                HandleAiming();
            }
        }

        private void HandleAiming()
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            aimTransform.eulerAngles = new Vector3(0, 0, angle);

            bool isFacingRight = transform.localScale.x < 0;
            if (mousePosition.x < transform.position.x)
            {
                aimTransform.localScale = isFacingRight ? new Vector3(-1, -1, 1) : new Vector3(1, -1, 1);
            }
            else
            {
                aimTransform.localScale = isFacingRight ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
        }

        private void FireBullet()
        {
            if (!canShoot) return; // Pastikan hanya bisa menembak di scene yang diperbolehkan
            if (isReloading) return;

            if (currentAmmo > 0)
            {
                ShootBullet();
                RemoveUIBullet();
                currentAmmo--;

                if (currentAmmo == 0)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        private void ShootBullet()
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab tidak ditemukan!", this);
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = (mousePosition - aimGunEndPointTransform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, aimGunEndPointTransform.position, Quaternion.identity);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                rigidbody.velocity = direction * bulletSpeed;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                Debug.LogError("Bullet tidak memiliki Rigidbody2D!", bullet);
            }

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamageMask(enemyLayer);
            }
            else
            {
                Debug.LogWarning("Bullet tidak memiliki komponen Bullet!", bullet);
            }

            if (aimAnimator != null) aimAnimator.SetTrigger("Shoot");

            if (weaponSoundEffect && AudioManager.Instance != null)
            {
                AudioManager.Instance.Play("22calgun");
            }

            OnShoot?.Invoke(this, new ShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }

        private void LoadUIBullets()
        {
            for (int i = 0; i < maxAmmo; i++)
            {
                SpawnUIBullet(i);
            }
        }

        private void SpawnUIBullet(int index)
        {
            if (uiBulletPrefab == null || reloadBar == null) return;

            GameObject uiBullet = Instantiate(uiBulletPrefab, reloadBar);
            RectTransform bulletRect = uiBullet.GetComponent<RectTransform>();

            if (bulletRect != null)
            {
                bulletRect.anchoredPosition = new Vector2(startX + (index * bulletSpacing), 0);
            }
        }

        private void RemoveUIBullet()
        {
            if (reloadBar.childCount > 0)
            {
                Destroy(reloadBar.GetChild(reloadBar.childCount - 1).gameObject);
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < maxAmmo; i++)
            {
                yield return new WaitForSeconds(reloadDelay);

                SpawnUIBullet(i);
                currentAmmo++;

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.Play("reload");
                }

                if (currentAmmo >= maxAmmo)
                {
                    break;
                }
            }
            isReloading = false;
        }
    }
}

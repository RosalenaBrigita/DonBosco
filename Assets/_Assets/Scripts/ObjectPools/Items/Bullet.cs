using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask ignoreMask;
        [Header("Settings")]
        [SerializeField] float damage = 20f;
        [SerializeField] float speed = 10f;
        [SerializeField] float lifeTime = 5f;

        private Rigidbody2D rb;
        private float lifeTimer;
        private LayerMask damageMask;
        private GameObject owner;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            lifeTimer = lifeTime;
        }

        private void Update()
        {
            lifeTimer -= Time.deltaTime;
            if(lifeTimer <= 0)
            {
                Pooling.Instance.ReturnBullet(this);
            }
            
            //Destroy the bullet if the owner is dead
            if(owner == null)
            {
                Pooling.Instance.ReturnBullet(this);
            }
        }

        void FixedUpdate()
        {
            rb.velocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Cek apakah owner ada dan apakah owner adalah player
            if (owner != null && owner.CompareTag("Player"))
            {
                Debug.Log($"[DEBUG] Bullet dari PLAYER mengenai: {other.gameObject.name}, Layer: {other.gameObject.layer}");
            }

            // Abaikan jika peluru mengenai owner sendiri
            if (other.gameObject == owner)
            {
                return;
            }

            if (damageMask == (damageMask | (1 << other.gameObject.layer)))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage, owner);

                    if (owner != null && owner.CompareTag("Player"))
                    {
                        Debug.Log($"[DEBUG] Player memberikan {damage} damage ke {other.gameObject.name}");
                    }
                }
            }

            Pooling.Instance.ReturnBullet(this);
        }



        public void SetDamageMask(LayerMask mask)
        {
            damageMask = mask;
        }

        public void SetAngle(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        public void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetOwner(GameObject owner)
        {
            this.owner = owner;
        }
    }
}

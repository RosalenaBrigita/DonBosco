using System.Collections;
using UnityEngine;

namespace DonBosco
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float damage = 20f;
        [SerializeField] private LayerMask damageMask;
        [SerializeField] private LayerMask ignoreMask;
        [SerializeField] private float lifeTime = 5f;

        private GameObject owner;

        public void SetDamageMask(LayerMask mask)
        {
            damageMask = mask;
        }

        public void SetOwner(GameObject owner)
        {
            if (owner == null)
            {
                Debug.LogWarning("PlayerBullet owner set to null! Ini bisa bikin error di NPC.");
            }
            this.owner = owner;
        }

        private void Start()
        {
            Destroy(gameObject, lifeTime); // hancur setelah waktu tertentu
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Abaikan jika mengenai owner sendiri
            if (other.gameObject == owner) return;

            // Abaikan jika layer target ada di ignoreMask
            if ((ignoreMask.value & (1 << other.gameObject.layer)) != 0)
            {
                Debug.Log($"[DEBUG] Peluru kena {other.gameObject.name}, tapi diabaikan (ignoreMask).");
                return;
            }

            // Lanjut hanya jika layer target ada di damageMask
            if ((damageMask.value & (1 << other.gameObject.layer)) != 0)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage, owner);

                    if (owner != null && owner.CompareTag("Player"))
                    {
                        Debug.Log($"[DEBUG] Player memberi {damage} damage ke {other.name}");
                    }
                }
            }

            Destroy(gameObject); // hancurkan peluru setelah menabrak sesuatu
        }
    }
}

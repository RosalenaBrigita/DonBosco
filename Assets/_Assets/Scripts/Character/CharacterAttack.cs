using System.Collections;
using System.Collections.Generic;
using DonBosco.Audio;
using UnityEngine;
using DonBosco.Dialogue;

namespace DonBosco.Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private bool weaponSoundEffect = false;
        [Header("Settings")]
        [SerializeField] public float fireDelay = 0.5f;
        [SerializeField] private float bulletDamage = 1f;

        private bool readyToFire = true;
        private float fireTimer = 0f;
        public float FireTimer => fireTimer;

        private float bonusDamage = 0f;

        private void Start()
        {
            CheckInkDamageBonus();
        }

        public void Attack(float angle, Vector3 startPosition)
        {
            if(!readyToFire) return;
            Bullet bullet = Pooling.Instance.GetBullet();
            bullet.SetOwner(gameObject);
            bullet.SetAngle(angle);
            bullet.SetDamage(bulletDamage + bonusDamage);
            bullet.SetStartPosition(startPosition);
            bullet.SetDamageMask(enemyLayer);

            // Start the fireTimer
            readyToFire = false;

            if(weaponSoundEffect)
            {
                AudioManager.Instance.Play("22calgun");
                StartCoroutine(PlayReloadSFx());
            }
        }

        private IEnumerator PlayReloadSFx()
        {
            yield return new WaitForSeconds(1.25f);
            AudioManager.Instance.Play("reload");
        }

        void Update()
        {
            if(!readyToFire)
            {
                FireDelay();
            }
        }

        // Countdown the fireTimer
        private void FireDelay()
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= fireDelay)
            {
                fireTimer = 0f;
                readyToFire = true;
            }
        }

        private void CheckInkDamageBonus()
        {
            var dialogueManager = DialogueManager.GetInstance();

            if (CompareTag("Ally") &&
                dialogueManager.GetVariableState("set_damage_bonus") is Ink.Runtime.BoolValue boolDamage &&
                boolDamage.value)
            {
                bonusDamage = 2f;
                Debug.Log($"[DAMAGE BONUS] Base: {bulletDamage}, Bonus: {bonusDamage}, Total: {bulletDamage + bonusDamage}");
            }
        }
    }
}
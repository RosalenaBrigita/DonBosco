using UnityEngine;
using CodeMonkey.Utils;

namespace DonBosco.Character
{
    public class PlayerShootEffects : MonoBehaviour
    {
        [SerializeField] private PlayerAimWeapon playerAimWeapon;

        private void Start()
        {
            // Subscribe to shooting event
            playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
        }

        private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.ShootEventArgs e)
        {
            // Cari objek HowToBulletTracer di scene
            HowToBulletTracer bulletTracer = FindObjectOfType<HowToBulletTracer>();
            if (bulletTracer != null)
            {
                bulletTracer.Shoot(e.gunEndPointPosition, e.shootPosition);
            }
        }

    }
}
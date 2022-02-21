using DataModels;
using Shared;
using UnityEngine;

namespace Items
{
    public class Launcher : MonoBehaviour, IWeapon
    {
        public RangedWeapon rangedWeapon;
        public GameObject missilePrefab;
        public Transform projectileParent;
        public Transform launchPoint;
        [Tooltip("The character or object launching missiles.")]
        public Transform launcher;
        [Tooltip("The force with which the gun shoots and the bullets fly.")]
        public float firePower;
        public bool autoFireOn;
        public bool isNpc;
        [Tooltip("The number of seconds between each shot.")]
        public float fireRate;
        public float delay;

        private float nextRound = 0;
        private GunState gunState = GunState.ReadyToFire;

        private void Update()
        {
            if (isNpc)
            {
                GetGunState();
                CountDown();
                if (autoFireOn && delay <= 0)
                {
                    AutoFire();
                }
            }
            if (Input.GetButtonDown("Fire1") && !isNpc)
                Shoot();
        }

        private void CountDown()
        {
            if (delay > 0)
                delay -= Time.deltaTime;
        }

        private void GetGunState()
        {
            switch (gunState)
            {
                case GunState.Shooting:
                    if (Time.time > nextRound)
                    {
                        gunState = GunState.ReadyToFire;
                    }
                    break;
                case GunState.Reset:
                    if (Time.time > nextRound)
                    {
                        gunState = GunState.ReadyToFire;
                    }
                    break;
            }
        }

        public RangedWeapon GetRangedWeapon()
        {
            return rangedWeapon;
        }
        
        public void AutoFire()
        {
            if (gunState.Equals(GunState.ReadyToFire))
            {
                gunState = GunState.Shooting;
                nextRound = Time.time + fireRate;
                Shoot();
            }
        }
        
        public void Shoot()
        {
            if (!launcher)
                launcher = transform;
            var missile = Instantiate(missilePrefab, launchPoint.position, launcher.rotation, projectileParent).GetComponent<Bullet>();
            var direction = launcher.localScale.x > 0 ? Vector2.right : Vector2.left;
            if (missile)
            {
                missile.rigidbody2d.AddRelativeForce(direction * firePower);
            }
        }
    }
}
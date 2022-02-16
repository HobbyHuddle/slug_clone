using System;
using System.Collections;
using System.ComponentModel;
using DataModels;
using InController.Scripts;
using Shared;
using UnityEngine;

namespace Items
{
    [Serializable]
    public enum GunState { ReadyToFire, Shooting, Reset }
    
    public class Gun : MonoBehaviour
    {
        public RangedWeapon rangedWeapon;
        public GameObject bulletPrefab;
        public Transform projectileParent;
        public Transform gunPoint;
        [Tooltip("The force with which the gun shoots and the bullets fly.")]
        public float firePower;
        public bool autoFireOn;
        public bool isNpc;
        [Tooltip("The number of seconds between each shot.")]
        public float fireRate;

        private Transform character;
        private float nextRound = 0;
        private GunState gunState = GunState.ReadyToFire;

        private void Start()
        {
            character = GetComponentInParent<CharacterController2D>().transform;
        }

        private void Update()
        {
            if (isNpc)
            {
                GetGunState();
                if (autoFireOn)
                    AutoFire();
            }
            if (Input.GetButtonDown("Fire1") && !isNpc)
                Shoot();
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
            // LESSON: What is Quaternion.identity? https://docs.unity3d.com/ScriptReference/Quaternion-identity.html
            var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity, projectileParent).GetComponent<Bullet>();
            var direction = character.localScale.x > 0 ? Vector2.right : Vector2.left;
            bullet.rigidbody2d.AddForce(direction * firePower);
        }
    }
}
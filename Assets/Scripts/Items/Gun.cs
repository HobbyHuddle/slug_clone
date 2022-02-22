using System;
using System.Collections;
using Characters;
using DataModels;
using InController.Scripts;
using Shared;
using UnityEngine;

namespace Items
{
    [Serializable]
    public enum GunState { ReadyToFire, Shooting, Reset }
    
    public class Gun : MonoBehaviour, IWeapon
    {
        public IWeapon weapon;
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
        public float delay;

        private Transform character;
        private float nextRound = 0;
        private GunState gunState = GunState.ReadyToFire;
        private AudioSource audio;
        private RangedWeapon originalWeapon;
        private CharacterController2D controller;

        private void Start()
        {
            character = GetComponentInParent<CharacterController2D>().transform;
            rangedWeapon = GetComponentInParent<BaseCharacter>().weapon;
            audio = GetComponent<AudioSource>();
            audio.clip = rangedWeapon.sfx;
            
            // FIXME: Temporary fix until input key refactor
            controller = character.gameObject.GetComponent<CharacterController2D>();
        }

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
            if (Input.GetButtonDown("Fire1") && !isNpc && !controller.IsDead)
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

        public void SwapWeapon(RangedWeapon newWeapon)
        {
            originalWeapon = rangedWeapon;
            rangedWeapon = newWeapon;
            audio.clip = rangedWeapon.sfx;
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
            var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity, projectileParent).GetComponent<Bullet>();
            var direction = character.localScale.x > 0 ? Vector2.right : Vector2.left;
            bullet.rigidbody2d.AddRelativeForce(direction * firePower);
            PlaySfx();
        }

        public void PlaySfx()
        {
            audio.Play();
        }
    }
}

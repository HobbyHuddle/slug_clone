using System;
using DataModels;
using InController.Scripts;
using Items;
using UnityEngine;

namespace Characters
{
    public class Gun : MonoBehaviour
    {
        public RangedWeapon rangedWeapon;
        public GameObject bulletPrefab;
        public Transform projectileParent;
        public Transform gunPoint;
        [Tooltip("The force with which the gun shoots and the bullets fly.")]
        public float firePower;

        private Transform character;

        private void Start()
        {
            character = GetComponentInParent<CharacterController2D>().transform;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
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

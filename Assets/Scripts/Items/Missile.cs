using System;
using System.Collections;
using System.Collections.Generic;
using DataModels;
using UnityEngine;

namespace Items
{
    public enum MissileOrientation { Vertical, Horizontal }
    public class Missile : MonoBehaviour
    {
        [Header("Missile")]
        public Rigidbody2D rigidbody2d;
        [Tooltip("The amount of seconds before the missile disappears.")]
        public float missileLifetime = 2;
        [Tooltip("The distance between projectile and target when they collide.")]
        public float collisionOffset = 1;
        public LayerMask targetLayers;
        public MissileOrientation orientation;
        private Animator animator;
        private Vector2 customDirection;
        
        private Vector2 CurrentDirection => rigidbody2d.velocity.x > 0 ? transform.right : -transform.right;
        private bool explode;
        private static readonly int Explode = Animator.StringToHash("explode");

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            GetOrientation();
            StartCoroutine(DestroyMissile());
        }

        private void Update()
        {
            // if (explode) animator.Play("Explode");
            animator.SetBool(Explode, explode);
            DestroyOnHit();
        }

        private void GetOrientation()
        {
            switch (orientation)
            {
                case MissileOrientation.Horizontal:
                    customDirection = transform.right;
                    break;
                case MissileOrientation.Vertical:
                    customDirection = transform.up;
                    break;
            }
        }

        private IEnumerator DestroyMissile()
        {
            yield return new WaitForSeconds(missileLifetime);
            explode = true;
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

        private void DestroyOnHit()
        {
            var direction = rigidbody2d.velocity.x > 0 ? customDirection : -customDirection;
            Ray2D ray = new Ray2D(transform.position, direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, direction, collisionOffset, targetLayers);
            
            Debug.DrawRay(ray.origin, direction * collisionOffset, Color.green);
            if (hit)
            {
                explode = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, customDirection * collisionOffset);
        }
    }
}
using System.Collections;
using DataModels;
using Shared;
using UnityEngine;

namespace Items
{
    public enum MissileOrientation { Vertical, Horizontal }
    public class Missile : Munition
    {
        [Header("Missile")]
        public MissileOrientation orientation;
        private Animator animator;
        private Vector2 customDirection;
        private AudioSource audio;
        
        private bool explode;
        private static readonly int Explode = Animator.StringToHash("explode");

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            audio = GetComponent<AudioSource>();
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
            yield return new WaitForSeconds(projectileLifetime);
            explode = true;
            audio.Play();
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
                audio.Play();
                // TODO: Still need to destroy this on hit. Currently relying on timeout.
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, customDirection * collisionOffset);
        }
    }
}
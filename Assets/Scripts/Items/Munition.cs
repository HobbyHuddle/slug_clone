using System.Collections;
using DataModels;
using Shared;
using UnityEngine;

namespace Items
{
    public class Bullet : MonoBehaviour, IProjectile
    {
        [Header("Bullet")]
        public Projectile bullet;
        public Rigidbody2D rigidbody2d;
        [Tooltip("The amount of seconds before the bullet disappears.")]
        public float bulletLifetime = 2;
        [Tooltip("The distance between projectile and target when they collide.")]
        public float collisionOffset = 1;
        public LayerMask targetLayers;
        
        private Vector2 CurrentDirection => rigidbody2d.velocity.x > 0 ? transform.right : -transform.right;
        private SpriteRenderer spriteRenderer;
        
        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.icon;
            StartCoroutine(DestroyBullet());
        }

        public Projectile GetProjectile()
        {
            return bullet;
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(bulletLifetime);
            Destroy(gameObject);
        }

        private void DestroyOnHit()
        {
            // FIXME: Currently, weapon position is further from player due to bullet collisions on spawn that prevent it from shooting.
            var direction = rigidbody2d.velocity.x > 0 ? transform.right : -transform.right;
            Ray2D ray = new Ray2D(transform.position, direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, direction, collisionOffset, targetLayers);
            
            Debug.DrawRay(ray.origin, direction * collisionOffset, Color.green);
            if (hit)
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, CurrentDirection * collisionOffset);
        }
    }
}
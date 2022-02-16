using System.Collections;
using DataModels;
using UnityEngine;

namespace Items
{
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet")]
        public Projectile bullet;
        public Rigidbody2D rigidbody2d;
        [Tooltip("The amount of seconds before the bullet disappears.")]
        public float bulletLifetime = 2;

        [Header("Bullet Mechanics")]
        public LayerMask targetLayers;
        [Range(0, 1)] public float castDistance = 0.25f;
        [Range(0, 1)] public float castRadius = 0.1f;
        
        
        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            StartCoroutine(DestroyBullet());
        }

        private void FixedUpdate()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(rigidbody2d.position, castRadius, Vector2.right, castDistance, targetLayers);
            Debug.Log("Bullet hits " + hits.Length);
            if (hits.Length > 0)
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(bulletLifetime);
            Destroy(gameObject);
        }
    }
}
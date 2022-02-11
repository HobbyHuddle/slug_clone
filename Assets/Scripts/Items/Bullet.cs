using System.Collections;
using DataModels;
using UnityEngine;

namespace Items
{
    public class Bullet : MonoBehaviour
    {
        public Projectile bullet;
        [Tooltip("The amount of seconds before the bullet disappears.")]
        public float bulletLifetime = 2;
        public Rigidbody2D rigidbody2d;

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            StartCoroutine(DestroyBullet());
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(bulletLifetime);
            Destroy(gameObject);
        }
    }
}
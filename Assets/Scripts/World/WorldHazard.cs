using InController.Scripts;
using UnityEngine;

namespace World
{
    public enum DamageType { Spikes }
    
    public class WorldHazard : MonoBehaviour
    {
        public DamageType damageType;
        public float damage;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                var player = col.GetComponent<CharacterController2D>();
                // player.Die();
            }
        }
    }
}
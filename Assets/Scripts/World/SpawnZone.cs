using Shared;
using UnityEngine;

namespace World
{
    /// <summary>
    /// Used to control spawns based on player entering and exiting the zone.
    /// </summary>
    public class SpawnZone : MonoBehaviour
    {
        public Spawner[] spawners;

        private void Start()
        {
            spawners = GetComponentsInChildren<Spawner>();
        }

        private void Activate()
        {
            foreach (Spawner spawner in spawners)
            {
                spawner.autoFireOn = true;
            }
        }
        
        private void Deactivate()
        {
            foreach (Spawner spawner in spawners)
            {
                spawner.autoFireOn = false;
                spawner.Reset();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                Activate();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                Deactivate();
        }
    }
}
using Shared;
using UnityEngine;

namespace DataModels
{
    /// <summary>
    /// Any weapon that can be used at range. Collectible.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Game/Gun", order = 0)]
    public class RangedWeapon : Item
    {
        public GameObject ammoPrefab;
        public int projectileCount;
        public int maxProjectiles;
        public int reloadAmount = 4;
        public float damage;
        public AudioClip sfx;
        
        /** When this item is collected and the player already has it equipped, the ammo is reloaded. */
        public void Reload()
        {
            if (projectileCount < maxProjectiles)
                projectileCount += reloadAmount;
        }
    }
}
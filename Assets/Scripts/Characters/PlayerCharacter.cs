using System;
using DataModels;
using Items;
using Shared;
using UnityEngine.Events;

namespace Characters
{
    [Serializable]
    public class HudUpdateEvent : UnityEvent<RangedWeapon> {}
    
    
    public class PlayerCharacter : BaseCharacter
    {
        public HudUpdateEvent onWeaponUpdate;
        // public PlayerDeathEvent onDeath;

        private void Start()
        {
            weapon = GetComponentInChildren<Gun>().rangedWeapon;
        }

        public void CollectItem(Item item)
        {
            if (item.itemType.Equals(ItemType.RangedWeapon))
            {
                if (weapon.Equals(item))
                    weapon.Reload();
                else
                    weapon = item as RangedWeapon;
                
                onWeaponUpdate.Invoke(weapon);
            }

            if (item.itemType.Equals(ItemType.HealthBoost))
            {
                var healthBooster = item as HealthBooster;
                UpdateHealth(healthBooster.healingPower);
            }
        }
    }
}
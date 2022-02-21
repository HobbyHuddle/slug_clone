using System;
using DataModels;
using Items;
using Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    [Serializable]
    public class HudUpdateEvent : UnityEvent<RangedWeapon> {}
    
    public class PlayerCharacter : MonoBehaviour
    {
        public float health;
        public RangedWeapon weapon;
        public HudUpdateEvent onWeaponUpdate;

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
        }

        // public void CollectItem(RangedWeapon rangedWeapon)
        // {
        //     if (rangedWeapon.itemType.Equals(ItemType.RangedWeapon))
        //     {
        //         if (weapon.Equals(rangedWeapon))
        //             weapon.Reload();
        //         else
        //             weapon = rangedWeapon;
        //     }
        // }
    }
}
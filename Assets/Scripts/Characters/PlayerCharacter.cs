using System;
using DataModels;
using Items;
using Resources;
using Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    [Serializable]
    public class HudUpdateEvent : UnityEvent<RangedWeapon> {}
    
    
    public class PlayerCharacter : BaseCharacter
    {
        public CharacterSfx sfx;
        public HudUpdateEvent onWeaponUpdate;

        private new AudioSource audio;

        private void Start()
        {
            audio = GetComponent<AudioSource>();
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

        public void PlayRunSfx()
        {
            audio.clip = sfx.run;
            audio.Play();
        }

        public void PlayDeathSfx()
        {
            audio.clip = sfx.die;
            audio.Play();
        }
    }
}
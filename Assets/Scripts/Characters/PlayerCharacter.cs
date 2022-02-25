using System;
using DataModels;
using Items;
using Resources;
using Shared;
using UnityEngine;
using UnityEngine.Events;
using World;

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

        private void OnTriggerEnter2D(Collider2D col)
        {
            // TODO: weapons damage, rations heal
            var projectileLayer = LayerMask.NameToLayer("Projectiles");
            var hazardsLayer = LayerMask.NameToLayer("Hazards");
            var colLayer = col.gameObject.layer;
            // FIXME: Refactor towards a boxcast or like to check all deadly layers.
            if (colLayer.Equals(projectileLayer))
            {
                var projectile = col.gameObject;
                var damage = projectile.GetComponent<Munition>().projectile.damage;
                Destroy(projectile);
                UpdateHealth(-damage);
            }

            if (colLayer.Equals(hazardsLayer))
            {
                var damage = col.gameObject.GetComponent<WorldHazard>().damage;
                UpdateHealth(-damage);
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
using System;
using System.Data;
using Characters;
using DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
  
    public class HudPanel : MonoBehaviour
    {
        public PlayerCharacter player;
        // public TextMeshProUGUI health;
        public TextMeshProUGUI score;
        public GameObject ammoRow;
        public GameObject ammoRowPrefab;

        private RangedWeapon currentWeapon;
        
        private void Start()
        {
            score.text = 0.ToString();
            InitializeAmmo();
        }

        private void InitializeAmmo()
        {
            currentWeapon = player.weapon;
            for (int i = 0; i < player.weapon.projectileCount; i++)
            {
                Instantiate(ammoRowPrefab, ammoRow.transform);
            }
        }

        private void ClearAmmo()
        {
            // TODO: Remove current ammo if new weapon is picked up
        }

        public void UpdateAmmo(RangedWeapon weapon)
        {
            if (currentWeapon.Equals(weapon))
            {
                var newAmmo = weapon.projectileCount - currentWeapon.projectileCount;
                for (int i = 0; i < newAmmo; i++)
                {
                    Instantiate(ammoRowPrefab, ammoRow.transform);
                }
            }
            else
            {
                for (int i = 0; i < weapon.projectileCount; i++)
                {
                    Instantiate(ammoRowPrefab, ammoRow.transform);
                }
            }
        }
        
    }
}
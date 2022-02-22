using Characters;
using DataModels;
using TMPro;
using UnityEngine;

namespace UI
{
  
    public class HudPanel : MonoBehaviour
    {
        public PlayerCharacter player;
        public TextMeshProUGUI score;
        public GameObject ammoRow;
        public GameObject ammoRowPrefab;

        private RangedWeapon currentWeapon;
        private const int iconLimit = 12;

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
            var ammoLimit = weapon.projectileCount > iconLimit ? iconLimit : weapon.projectileCount;
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
                for (int i = 0; i < ammoLimit; i++)
                {
                    Instantiate(ammoRowPrefab, ammoRow.transform);
                }
            }
        }
        
    }
}
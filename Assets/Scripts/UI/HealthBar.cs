using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public PlayerCharacter character;
        public Image healthBar;

        private void Start()
        {
            healthBar = healthBar.GetComponentInChildren<Image>();
        }

        private void Update()
        {
            healthBar.fillAmount = character.health / character.maxHealth; 
        }
    }
}
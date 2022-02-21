using System;
using Characters;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameDebugPanel : MonoBehaviour
    {
        public PlayerCharacter player1;
        public int healthBoosterAmount;
        public TextMeshProUGUI healthBoosterText;

        private void Start()
        {
            healthBoosterText.text = "+" + healthBoosterAmount + " Health";
        }

        public void HealthBoost()
        {
            player1.health += healthBoosterAmount;
        }
    }
}
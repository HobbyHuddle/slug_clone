using System;
using DataModels;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    [Serializable]
    public class PlayerDeathEvent : UnityEvent {}
    
    public abstract class BaseCharacter : MonoBehaviour
    {
        public float health = 1;
        public RangedWeapon weapon;
        public PlayerDeathEvent onDeath;
        
        private void Start()
        {
            weapon = GetComponentInChildren<Gun>().rangedWeapon;
        }
        
        public void UpdateHealth(float num)
        {
            health += num;
            if (health < 0)
            {
                onDeath.Invoke();
            }
        }
    }
}
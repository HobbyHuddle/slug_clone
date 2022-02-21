using System;
using UnityEngine;

namespace Shared
{
    [Serializable]
    public enum ItemType { Projectile, RangedWeapon, HealthBoost }
    
    public abstract class Item : ScriptableObject
    {
        public Sprite icon;
        public string description;
        public ItemType itemType;
    }
}
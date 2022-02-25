using System;
using UnityEngine;

namespace Shared
{
    [Serializable]
    public enum ItemType { Projectile, RangedWeapon, HealthBoost, POW }
    
    public abstract class Item : ScriptableObject
    {
        public Sprite icon;
        public Sprite image;
        public string description;
        public ItemType itemType;
    }
}
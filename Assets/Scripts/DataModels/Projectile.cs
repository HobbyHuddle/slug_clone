using UnityEngine;

namespace DataModels
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Game/Projectile", order = 0)]
    public class Projectile : ScriptableObject
    {
        public Sprite icon;
        public string description;
        public float damage;
    }
}
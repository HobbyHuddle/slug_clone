using UnityEngine;

namespace DataModels
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Game/Gun", order = 0)]
    public class RangedWeapon : ScriptableObject
    {
        public Sprite icon;
        public string description;
    }
}
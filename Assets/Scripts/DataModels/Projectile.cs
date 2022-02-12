using System;
using Shared;
using UnityEngine;

namespace DataModels
{
    /// <summary>
    /// All projectiles are used to render game objects and are non-collectible items.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Game/Projectile", order = 0)]
    public class Projectile : Item
    {
    }
}
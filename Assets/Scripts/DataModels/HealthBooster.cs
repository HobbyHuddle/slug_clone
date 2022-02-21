using Shared;
using UnityEngine;

namespace DataModels
{
    [CreateAssetMenu(fileName = "New Health Booster", menuName = "Game/Health Booster", order = 0)]
    public class HealthBooster : Item
    {
        public float healingPower = 1;
    }
}
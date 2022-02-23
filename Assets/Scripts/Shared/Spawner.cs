using UnityEngine;

namespace Shared
{
    public class Spawner : MonoBehaviour
    {
        public bool autoFireOn;
        public float delay;
        [ReadOnlyField] public float counter;
        
        public void Reset()
        {
            counter = delay;
        }
    }
}
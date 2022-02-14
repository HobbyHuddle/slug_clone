using UnityEngine;

namespace Shared
{
    public class Patrol : MonoBehaviour
    {
        public Transform leftPoint;
        public Transform rightPoint;
        public float speed = 3f;

        public bool atLeft = true;

        public bool atRight;
    }
}
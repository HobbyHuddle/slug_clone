using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace World
{
    public class MovingPlatform : MonoBehaviour
    {
        public Transform waypointParent;
        public List<Waypoint> waypoints; // TODO: Make read only field
        public float speed = 1.0f;
        [Tooltip("Turn automatic movement on or off.")]
        public bool automatic = true;

        private Vector2 currentDestination;
        private Queue<Waypoint> forwardTrack;
        private Stack<Waypoint> backTrack;
        private bool goForward = true;
        private bool goBack;

        // Start is called before the first frame update
        void Start()
        {
            waypoints = waypointParent.GetComponentsInChildren<Waypoint>().ToList();
            forwardTrack = new Queue<Waypoint>(waypoints);
            backTrack = new Stack<Waypoint>(waypoints);
            currentDestination = waypointParent.position;
        }

        void FixedUpdate()
        {
            Vector2 pos = transform.position;
            
            if (automatic)
            {
                // Only move to the next waypoint if platform has arrived at current destination.
                if (pos != currentDestination)
                {
                    Goto(currentDestination);
                }
                else
                {
                    if (goForward)
                    {
                        currentDestination = MoveForward();
                    }

                    if (goBack)
                    {
                        currentDestination = MoveBack();
                    }
                }
            }
        }

        private void Goto(Vector2 nextDestination)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextDestination, speed * Time.deltaTime);
        }

        private Vector2 MoveForward()
        {
            if (forwardTrack.Count == 0)
            {
                forwardTrack = new Queue<Waypoint>(waypoints);
                goBack = true;
                goForward = false;
                return currentDestination;
            }
            return forwardTrack.Dequeue().transform.position;
        }
        
        private Vector2 MoveBack()
        {
            if (backTrack.Count == 0)
            {
                backTrack = new Stack<Waypoint>(waypoints);
                goBack = false;
                goForward = true;
                return currentDestination;
            }
            return backTrack.Pop().transform.position;
        }
        
        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.transform.SetParent(transform, true);
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.transform.SetParent(null);
            }
        }
    }
}

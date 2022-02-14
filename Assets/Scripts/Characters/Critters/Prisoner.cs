using InController.Scripts;
using Items;
using UnityEngine;

/// <summary>
/// Arcturus Dev Note: 
/// I know it says that critter behaviour is not required on the trello board (wasn't 100% sure why)...
/// And that I might have done it anyway...
/// But don't worry! It'll be super easy to remove it if need be (maybe?).
/// It can be built into a base class later too if need be.
/// </summary>

namespace Characters
{
    public class Prisoner : MonoBehaviour
    {
        enum PrisonerBehaviour { Stuck, Roam, Rewarding, Escaping } // Defining enum.
        PrisonerBehaviour behaviour; // The basic prisoner behaviour that controls its actions.

        [Header("Logic vars")]
        [SerializeField] CharacterController2D controller;

        [Header("Patrol Vars")]
        // FIXME: Convert this script to use inheritance from a base critter class.
        [SerializeField] Vector2[] patrolPoints; // The points that the critter will patrol between.
        Vector2 myOrigin; // The character will patrol a certain distance from where it started. Also allows for patrolling characters to fall off ledges.
        Vector2 myTarget; // The point the character will walk towards.
        int patrolIndex = 0; // The index of the current patrol point.

        private void Start()
        {
            // Remember our origin.
            myOrigin = new Vector2(transform.position.x, transform.position.y);
            myTarget = new Vector2(patrolPoints[patrolIndex].x + myOrigin.x, 0f);

            behaviour = PrisonerBehaviour.Stuck;
            // Ping animator to play stuck animation in loop.
        }

        // The logic update.
        private void Update()
        {
            switch (behaviour)
            {
                case PrisonerBehaviour.Roam:
                    // Check if we are at the current target.
                    // If we are then go to the next one.
                    if (Vector2.Distance(new Vector2(transform.position.x, 0), myTarget) <= 0.5f)
                    {
                        patrolIndex++; // Step forwards in the array.

                        if (patrolIndex >= patrolPoints.Length)
                            patrolIndex = 0; // Loops back to the first index.
                        
                        myTarget = new Vector2(patrolPoints[patrolIndex].x + myOrigin.x, 0f);
                    }
                    break;

                case PrisonerBehaviour.Rewarding:
                    // Ping the animator to play the reward animation
                    // Spawn the player's reward.
                    Debug.Log("Enjoy your reward!");
                    behaviour = PrisonerBehaviour.Escaping;
                    break;

                case PrisonerBehaviour.Escaping:
                    break;
            }
        }

        // The physics update.
        private void FixedUpdate()
        {
            switch (behaviour)
            {
                case PrisonerBehaviour.Roam:
                    // Check if we are on the ground.
                    // if we are then walk
                    if (controller.IsGrounded) // FIXME: Doesn't actually seem to detect anything since always returns true here?????
                    {
                        // We can walk around.
                        var direction = new Vector2(Mathf.Clamp(myTarget.x - transform.position.x, -1, 1), 0);
                        //rb.velocity = direction;
                        controller.Move(direction, false, false);
                    }
                    else
                    {
                        // Ping animator that it needs to play the falling anim.
                        Debug.Log("Still falling.");
                    }
                    break;

                case PrisonerBehaviour.Escaping:
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Need to check for melee attack too, but can modify later.
            if (behaviour == PrisonerBehaviour.Stuck && collision.transform.GetComponent<Bullet>()) 
            {
                Destroy(collision.gameObject); // TEMP

                // We want to "free" the prisoner from their bindings.
                behaviour = PrisonerBehaviour.Roam;
                return;
            }

            // Using tag comparison for now but rather use a defining component.
            if (behaviour == PrisonerBehaviour.Roam && collision.transform.CompareTag("Player")) 
            {
                // We want the prisoner to reward us then GTFO.
                behaviour = PrisonerBehaviour.Rewarding;
                return;
            }
        }
    }
}

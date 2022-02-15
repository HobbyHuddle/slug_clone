using InController.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class Critter : MonoBehaviour
    {
        [Header("Logic vars")]
        [SerializeField] CharacterController2D controller;

        [Header("Patrol Vars")]
        [SerializeField] float[] patrolPoints; // The offsets from the critter's original position to move;
        Vector2 myOrigin; // The character will patrol a certain distance from where it started. Also allows for patrolling characters to fall off ledges.
        Vector2 myTarget; // The point the character will walk towards.
        int patrolIndex = 0; // The index of the current patrol point.

        protected virtual void Start()
        {
            // Remember our origin.
            myOrigin = new Vector2(transform.position.x, transform.position.y);
            myTarget = new Vector2(patrolPoints[patrolIndex] + myOrigin.x, 0f);
        }

        protected void PatrolUpdateLogic()
        {
            // Check if we are at the current target.
            // If we are then go to the next one.
            if (Vector2.Distance(new Vector2(transform.position.x, 0), myTarget) <= 0.5f)
            {
                patrolIndex++; // Step forwards in the array.

                if (patrolIndex >= patrolPoints.Length)
                    patrolIndex = 0; // Loops back to the first index.

                myTarget = new Vector2(patrolPoints[patrolIndex] + myOrigin.x, 0f);
            }
        }

        protected void PatrolFixedUpdateLogic()
        {
            // Check if we are on the ground.
            // if we are then walk
            if (controller.IsGrounded)
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
        }
    }
}

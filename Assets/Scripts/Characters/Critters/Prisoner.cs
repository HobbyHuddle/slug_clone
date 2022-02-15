using InController.Scripts;
using Items;
using UnityEngine;

namespace Characters
{
    public class Prisoner : Critter
    {
        enum PrisonerBehaviour { Stuck, Roam, Rewarding, Escaping } // Defining enum.
        PrisonerBehaviour behaviour; // The basic prisoner behaviour that controls its actions.

        protected override void Start()
        {
            base.Start();

            behaviour = PrisonerBehaviour.Stuck;
            // Ping animator to play stuck animation in loop.
        }

        // The logic update.
        private void Update()
        {
            switch (behaviour)
            {
                case PrisonerBehaviour.Roam:
                    PatrolUpdateLogic();
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
                    PatrolFixedUpdateLogic();
                    break;

                case PrisonerBehaviour.Escaping:
                    // TODO: Get the critter to run.
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

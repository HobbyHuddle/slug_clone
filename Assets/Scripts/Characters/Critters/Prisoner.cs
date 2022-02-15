using InController.Scripts;
using Items;
using UnityEngine;

namespace Characters
{
    public class Prisoner : Critter
    {
        enum PrisonerBehaviour { Stuck, Roam, Rewarding, Escaping } // Defining enum.
        PrisonerBehaviour behaviour; // The basic prisoner behaviour that controls its actions.

        [SerializeField] Vector2 escapeDirection;
        [SerializeField] float escapeTime = 3f;
        [SerializeField] float fadeSpeed = 1f;
        float count;

        SpriteRenderer sr;

        protected override void Start()
        {
            base.Start();

            count = escapeTime;
            sr = GetComponentInChildren<SpriteRenderer>();

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

                    // Set movespeed to run.
                    controller.speed *= 1.5f;

                    behaviour = PrisonerBehaviour.Escaping;
                    break;

                case PrisonerBehaviour.Escaping:
                    count -= Time.deltaTime;
                    if (count <= 0f)
                    {
                        // Fade out and destroy.
                        sr.color = new Color(1, 1, 1, Mathf.Lerp(sr.color.a, 0, fadeSpeed * Time.deltaTime));
                        if (sr.color.a <= 0.01f)
                            Destroy(gameObject);
                    }
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
                    // Get the critter to run.
                    controller.Move(escapeDirection, false, false);
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
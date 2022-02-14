using System;
using System.Collections;
using InController.Scripts;
using Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    [Serializable]
    public class AttackEvent : UnityEvent {}
    
    public class EnemyController : CharacterController2D
    {
        public CharacterController2D target;
        public Patrol patrol;
        public AttackEvent inAttackRange;
        
        public bool FaceLeft { get => IsFacingLeft; set => facingLeft = value; }
        public bool Walk { get => walking; set => walking = value; }
        public float aggroDistance = 3f;
        public Vector3 attackOffset = new Vector3 { x = -1 };
        public float attackRange = 2f;
        public float sightRange = 8f;
        public bool isHostile = true;
        public bool isPatrolling;
        
        private SpriteRenderer spriteRenderer;
        
        private bool InSightRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < sightRange;
        private bool InAggroRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < aggroDistance;
        private bool InAttackRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < attackRange;
        
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentScale = rigidbody2d.transform.localScale;
        }
        
        private void Update()
        {
            if (IsHurt)
            {
                hurt = false;
            }
            
            if (IsDead)
            {
                motion = new Vector2();
                dead = true;
                gameObject.layer = LayerMask.NameToLayer("Untouchable");
                SetAnimationState();
                return;
            }

            if (InSightRange)
            {
                facingLeft = !(target.transform.position.x - rigidbody2d.position.x > 0);
            }
            ChangeFaceDirection();
            if (isHostile)
            {
                if (InAttackRange)
                {
                    chasing = false;
                    inAttackRange.Invoke();
                    StartCoroutine(AttackOnCooldown());
                }
                if (InAggroRange)
                {
                    attacking = false;
                    chasing = true;
                }
                else
                {
                    chasing = false;
                }
            }
            
            SetAnimationState();
        }

        private void FixedUpdate()
        {
            if (isPatrolling)
            {
                if (InSightRange)
                {
                    walking = false;
                    return;
                }
                if (patrol.atRight)
                {
                    PatrolLeft();
                } 
                if (patrol.atLeft)
                {
                    PatrolRight();
                }
                walking = true;
            }
            ChangeFaceDirection();
        }

        private void OnDrawGizmos()
        {
            Vector2 direction = IsFacingLeft ? new Vector2 { x = -aggroDistance }: new Vector2 { x = aggroDistance};
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, direction);
        }

        private IEnumerator AttackOnCooldown()
        {
            attacking = true;
            yield return new WaitForSeconds(0.5f);
            attacking = false;
            yield return new WaitForSeconds(2f);
        }

        private void PatrolLeft()
        {
            Vector2 destination = patrol.leftPoint.position;
            Debug.Log("Patrolling left.");
            FaceLeft = true;
            rigidbody2d.position = Vector2.MoveTowards(rigidbody2d.position, destination, patrol.speed * Time.deltaTime);
            if (rigidbody2d.position.x < destination.x + 1) // FIXME: workaround -- position resets to absolute so comparison fails otherwsie
            {
                patrol.atRight = false;
                patrol.atLeft = true;
            }
        }
        
        private void PatrolRight()
        {
            Vector2 destination = patrol.rightPoint.position;
            Debug.Log("Patrolling right.");
            FaceLeft = false;
            rigidbody2d.position = Vector2.MoveTowards(rigidbody2d.position, destination, patrol.speed * Time.deltaTime);
            if (rigidbody2d.position.x >= destination.x)
            {
                patrol.atRight = true;
                patrol.atLeft = false;
            }
        }

        public void Shoot()
        {
            
        }
    }
}
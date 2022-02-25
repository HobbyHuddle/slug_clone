using System;
using System.Collections;
using DataModels;
using InController.Scripts;
using Items;
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
        public LayerMask deadlyLayers;
        public AttackEvent inAttackRange;
        
        public bool FaceLeft { get => IsFacingLeft; set => facingLeft = value; }
        public bool Walk { get => walking; set => walking = value; }
        [Tooltip("Becomes aware of another character, non-aggressively.")]
        public float sightRange = 8f;
        [Tooltip("Will approach another, but not attack.")]
        public float aggroRange = 3f;
        [Tooltip("Will attack at this distance.")]
        public float attackRange = 2f;
        public Vector3 attackOffset = new Vector3 { x = -1 };
        public float corpseTimer = 1f;
        public bool isHostile = true;
        public bool isPatrolling;
        
        private SpriteRenderer spriteRenderer;
        private Enemy enemy;
        
        // FIXME: each of this requires a target null check. refactor usage
        private bool InSightRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < sightRange;
        private bool InAggroRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < aggroRange;
        private bool InAttackRange => Vector2.Distance(rigidbody2d.position, target.transform.position) < attackRange;
        
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentScale = rigidbody2d.transform.localScale;
            enemy = GetComponentInParent<Enemy>();
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
                StartCoroutine(RemoveCorpse());
                SetAnimationState();
                return;
            }

            if (target && InSightRange)
            {
                facingLeft = !(target.transform.position.x - rigidbody2d.position.x > 0);
            }
            ChangeFaceDirection();
            if (isHostile && target)
            {
                if (InAttackRange)
                {
                    chasing = false;
                    StartCoroutine(AttackOnCooldown());
                    inAttackRange.Invoke();
                }
                if (InAggroRange & !InAttackRange)
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
            if (!IsDead && isPatrolling)
            {
                if (target && InSightRange)
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
            Vector2 direction = IsFacingLeft ? new Vector2 { x = -aggroRange }: new Vector2 { x = aggroRange};
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, direction);
        }

        private IEnumerator RemoveCorpse()
        {
            yield return new WaitForSeconds(corpseTimer);
            Destroy(enemy.gameObject); 
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
            FaceLeft = false;
            rigidbody2d.position = Vector2.MoveTowards(rigidbody2d.position, destination, patrol.speed * Time.deltaTime);
            if (rigidbody2d.position.x >= destination.x)
            {
                patrol.atRight = true;
                patrol.atLeft = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var mask = LayerMask.NameToLayer("Projectiles");
            var projectile = col.gameObject;
            if (col.gameObject.layer.Equals(mask))
            {
                var damage = projectile.GetComponent<Munition>().projectile.damage;
                Debug.Log("Projectile damage: " + damage);
                Destroy(projectile);
                onHealthChange.Invoke(-damage);
            }
        }
    }
}
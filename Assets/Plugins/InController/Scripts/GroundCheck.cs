using UnityEngine;

namespace InController.Scripts
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] public LayerMask targetLayer;
        public BoxCollider2D boxCollider;
        [Range(-0.5f, 0.75f)] public float distanceToGround;

        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, Vector2.down, distanceToGround,targetLayer);
            //Debug.Log("Ground check: " + hit.collider);
            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.size);
        }
    }
}

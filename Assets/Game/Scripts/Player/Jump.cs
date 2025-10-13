using UnityEngine;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundMask;

        private Rigidbody _rigidbbody;

        private void Start()
        {
            _rigidbbody = GetComponent<Rigidbody>();
        }
        public void TryToJump()
        {
            if (IsGrounded())
            {
                _rigidbbody.linearVelocity = new Vector3(_rigidbbody.linearVelocity.x, 0f, _rigidbbody.linearVelocity.z);
                _rigidbbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        }
    }
}
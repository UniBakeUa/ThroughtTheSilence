using UnityEngine;

namespace Player
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;

        private Vector2 _input;
        private Rigidbody _rigidbody;
        private bool _isSprinting;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
        }
        private void FixedUpdate()
        {
            Vector3 moveDir = transform.right * _input.x + transform.forward * _input.y;
            float speed = _isSprinting ? sprintSpeed : walkSpeed;

            Vector3 targetVel = moveDir.normalized * speed;
            _rigidbody.linearVelocity = new Vector3(targetVel.x, _rigidbody.linearVelocity.y, targetVel.z);
        }
        public void ChangeInput(Vector2 input) => _input = input;

        public void SetSprint(bool state) => _isSprinting = state;
    }
}
using UnityEngine;

namespace Player
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float crouchHeight;
        [SerializeField] private float crouchSpeed;
        [SerializeField] private LayerMask whatIsSolid;

        private float standHeight;
        private Vector2 _input;
        private Rigidbody _rigidbody;
        private CapsuleCollider _capsule;
        private bool _isSprinting;
        private bool _isCrouching;
        private float _currentHeight;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            _capsule = GetComponent<CapsuleCollider>();

            standHeight = _capsule.height;
            _currentHeight = standHeight;
            
            _capsule.center = new Vector3(0f, 0f, 0f);
            
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }



        private void FixedUpdate()
        {
            MovePlayer();
            HandleCrouch();
        }

        public void ChangeInput(Vector2 input) => _input = input;

        public void SetSprint(bool state) => _isSprinting = state;

        public void ToCrouch()
        {
            _isCrouching = true;
        }

        public void ToStand()
        {
            if (CanStand()) _isCrouching = false;
        }

        private void MovePlayer()
        {
            Vector3 moveDir = transform.right * _input.x + transform.forward * _input.y;
            float speed = _isSprinting ? sprintSpeed : walkSpeed;
            if (_isCrouching) speed *= 0.5f;

            Vector3 targetVel = moveDir.normalized * speed;
            _rigidbody.linearVelocity = new Vector3(targetVel.x, _rigidbody.linearVelocity.y, targetVel.z);
        }

        public void HandleCrouch()
        {
            float targetHeight = _isCrouching ? crouchHeight : standHeight;
            float targetCenterY = _isCrouching ? 0.5f : 0f;
            
            _currentHeight = Mathf.Lerp(_currentHeight, targetHeight, Time.fixedDeltaTime * crouchSpeed);
            _capsule.height = _currentHeight;
            
            Vector3 currentCenter = _capsule.center;
            currentCenter.y = Mathf.Lerp(currentCenter.y, targetCenterY, Time.fixedDeltaTime * crouchSpeed);
            _capsule.center = currentCenter;
        }



        private bool CanStand()
        {
            float radius = _capsule.radius * 0.5f;
            
            Vector3 top = transform.position + Vector3.up * standHeight;
            
            Collider[] hits = Physics.OverlapSphere(top, radius, whatIsSolid);
    
            return hits.Length == 0;
        }
    }
}
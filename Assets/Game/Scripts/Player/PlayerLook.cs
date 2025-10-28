using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float sensitivity;
        [SerializeField] private float verticalLimit;
        public static bool canLook = true;
        private float _pitch;
        private Vector2 _lookInput;

        public void ChangeInput(Vector2 input) => _lookInput = input;

        private void LateUpdate()
        {
            if (canLook)
            {
                float mouseX = _lookInput.x * sensitivity * Time.deltaTime;
                float mouseY = _lookInput.y * sensitivity * Time.deltaTime;

                _pitch -= mouseY;
                _pitch = Mathf.Clamp(_pitch, -verticalLimit, verticalLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
                transform.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
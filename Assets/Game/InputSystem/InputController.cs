using UnityEngine;

namespace Game.InputSystem
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance;
        public PlayerControls gameInput { get; private set; }

        private void Awake()
        {
            Instance = this;

            gameInput = new PlayerControls();
            gameInput.Enable();
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}
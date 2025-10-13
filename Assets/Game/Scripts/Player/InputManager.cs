using Game.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
            private InputController _controls;
            
            private Move _move;
            private PlayerLook _look;
            private Jump _jump;

            private void Start()
            {
                _controls = InputController.Instance;
                _move = GetComponent<Move>();
                _look = GetComponent<PlayerLook>();
                _jump = GetComponent<Jump>();

                SetInputs();
            }

            private void SetInputs()
            {
                PlayerControls.GamePlayActions gameplay = _controls.gameInput.GamePlay;

                // Move
                gameplay.Move.performed += OnMoveChanged;
                gameplay.Move.canceled += OnMoveChanged;

                // Sprint
                gameplay.Sprint.performed += ctx => _move.SetSprint(true);
                gameplay.Sprint.canceled += ctx => _move.SetSprint(false);
                
                // Camera
                gameplay.Look.performed += OnLookChanged;
                gameplay.Look.canceled += OnLookChanged;

                // Jump
                gameplay.Jump.performed += OnJump;
            }

            private void OnDestroy()
            {
                LeaveInputs();
            }

            private void LeaveInputs()
            {
                var gameplay = _controls.gameInput.GamePlay;

                gameplay.Move.performed -= OnMoveChanged;
                gameplay.Move.canceled -= OnMoveChanged;

                gameplay.Sprint.performed -= ctx => _move.SetSprint(true);
                gameplay.Sprint.canceled -= ctx => _move.SetSprint(false);
                
                
                gameplay.Look.performed -= OnLookChanged;
                gameplay.Look.canceled -= OnLookChanged;

                gameplay.Jump.performed -= OnJump;
            }

            private void OnMoveChanged(InputAction.CallbackContext ctx)
                => _move.ChangeInput(ctx.ReadValue<Vector2>());
            
            private void OnLookChanged(InputAction.CallbackContext ctx)
                => _look.ChangeInput(ctx.ReadValue<Vector2>());

            private void OnJump(InputAction.CallbackContext ctx)
                => _jump.TryToJump();
    }
}
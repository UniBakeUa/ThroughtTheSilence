using System;
using Game.Scripts;
using UnityEngine;

namespace Player
{
    public class Interacter : MonoBehaviour
    {
        private Camera cam;

        public float maxDistance = 3f;
        public LayerMask interactLayerMask = ~0;

        public event Action<IInteractable> OnInteract;

        private IInteractable _previousTarget;
        private IInteractable _currentTarget;

        public void TryToInteract()
        {
            if (_currentTarget != null)
            {
                _currentTarget.Interact();
                OnInteract?.Invoke(_currentTarget);
            }
        }

        private void Awake()
        {
            if (cam == null)
            {
                cam = Camera.main;
                if (cam == null)
                    Debug.LogError("camera missing");
            }
        }

        private void Update()
        {
            RaycastHit hit;
            var hitTarget = RaycastCenter(out hit);
            
            if (hitTarget != null && hitTarget is IInteractable interactable)
            {
                _currentTarget = interactable;
            }
            else
            {
                _currentTarget = null;
            }
            
            if (_previousTarget != _currentTarget)
            {
                _previousTarget?.Unhighlight();
                _currentTarget?.Highlight();
                _previousTarget = _currentTarget;
            }
        }



        private IInteractable RaycastCenter(out RaycastHit hitInfo)
        {
            hitInfo = default;
            if (cam == null) return null;

            Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(center);

            if (Physics.Raycast(ray, out hitInfo, maxDistance, interactLayerMask))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out var interactable))
                    return interactable;
            }

            return null;
        }
    }
}
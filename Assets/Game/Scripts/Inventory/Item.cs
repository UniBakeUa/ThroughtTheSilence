using UnityEngine;

namespace Game.Scripts
{
    public class Item : MonoBehaviour,IInteractable
    {
        [SerializeField] private Material highlightMaterial;
        
        private Material normalMaterial;
        private Renderer _renderer;
        private bool _highlighted;
        
        public void Interact()
        {
            Debug.Log("Interacting...");
            Destroy(gameObject);
        }

        public void Highlight()
        {
            if (_highlighted) return;
            _renderer.material = highlightMaterial;
            _highlighted = true;
        }

        public void Unhighlight()
        {
            if (!_highlighted) return;
            if(!_renderer) return;
            _renderer.material = normalMaterial;
            _highlighted = false;
        }
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            if (normalMaterial == null)
                normalMaterial = _renderer.sharedMaterial;
        }
    }
}
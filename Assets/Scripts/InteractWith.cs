using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InteractWith : MonoBehaviour
    {
        public new Camera camera;
        public float detectionDistance = 2f;

        public InputActionAsset actionAsset;
        private InputAction _interactAction;
        private IInteractable _interactable;

        private void Awake()
        {
            _interactAction = InputSystem.actions.FindAction("Interact");
            _interactAction.performed += TryInteract;
        }

        void Update()
        {
            var ray = new Ray(camera.transform.position, camera.transform.forward);
        
            if (Physics.Raycast(ray, out RaycastHit hitResult, detectionDistance))
            {
                _interactable = hitResult.transform.GetComponent<IInteractable>();
                if (_interactable != null && _interactable.CanInteractWith())
                {
                    //Show interact widget
                    print(_interactable);
                    //interactable.Interact(); // or whatever your method is
                }
            }
        }

        private void TryInteract(InputAction.CallbackContext callbackContext)
        {
            print("Interact");
            _interactable?.Interact();
        }
    }
}
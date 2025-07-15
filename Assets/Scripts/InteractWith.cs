using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InteractWith : MonoBehaviour
    {
        public new Camera camera;
        public float detectionDistance = 2f;

        private InputAction _interactAction;
        private IInteractable _interactable;
        private Coroutine _interactCoroutine;
        
        private InteractWidget _interactWidget;
        
        private void Awake()
        {
            _interactAction = InputSystem.actions.FindAction("Interact");
            _interactAction.started += TryInteract;
            _interactAction.canceled += CancelInteract;
            
            _interactWidget  = FindFirstObjectByType<InteractWidget>();
        }

        void Update()
        {
            var ray = new Ray(camera.transform.position, camera.transform.forward);
        
            if (Physics.Raycast(ray, out RaycastHit hitResult, detectionDistance))
            {
                _interactable = hitResult.transform.GetComponent<IInteractable>();
                if (_interactable == null)
                {
                    StopInteract();
                }
                else if (_interactable != null && _interactable.CanInteractWith())
                {
                    _interactWidget.Show();
                }
            }
            else
            {
                StopInteract();
            }
        }

        private void TryInteract(InputAction.CallbackContext callbackContext)
        {
            if (_interactable is not null && _interactable.StartInteraction())
                _interactCoroutine = StartCoroutine(Interaction(_interactable.timeOfInteraction));
        }

        private void CancelInteract(InputAction.CallbackContext callbackContext)
        {
            _interactWidget.isInteracting = false;
            if (_interactCoroutine != null) StopCoroutine(_interactCoroutine);
        }

        private IEnumerator Interaction(float time)
        {
            var elapsed = 0f;
            while (elapsed <= time)
            {
                elapsed += Time.deltaTime;
                _interactWidget.UpdateInteractProgress(time,  elapsed);
                _interactWidget.isInteracting = true;
                yield return null;
            }
            _interactable?.Interact();
            StopInteract();
        }

        private void StopInteract()
        {
            _interactable = null;
            _interactWidget.Hide();
            _interactWidget.isInteracting = false;
            if (_interactCoroutine != null) StopCoroutine(_interactCoroutine);
        }
    }
}
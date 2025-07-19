using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class InteractWith : MonoBehaviour
    {
        public new Camera camera;
        public float detectionDistance = 2f;
        public AudioSource interactionSource;

        private InputAction _interactAction;
        private IInteractable _interactable;
        private Coroutine _interactCoroutine;
        
        private InteractWidget _interactWidget;
        private ThoughtsWidget _thoughtsWidget;

        private void Awake()
        {
            _interactAction = InputSystem.actions.FindAction("Interact");
            _interactAction.started += TryInteract;
            _interactAction.canceled += CancelInteract;
            
            _interactWidget  = FindFirstObjectByType<InteractWidget>();
            _thoughtsWidget  = FindFirstObjectByType<ThoughtsWidget>();
        }

        void Update()
        {
            var ray = new Ray(camera.transform.position, camera.transform.forward);
        
            if (Physics.Raycast(ray, out RaycastHit hitResult, detectionDistance))
            {
                hitResult.transform.TryGetComponent(out _interactable);
                if (_interactable == null)
                {
                    StopInteract();
                }
                if (_interactable != null && _interactable.CanInteractWith())
                {
                    _interactWidget.Show();
                    _interactWidget.UpdateInteractText(_interactable.interactionText);
                }
            }
            else
            {
                StopInteract();
            }
        }

        private void TryInteract(InputAction.CallbackContext callbackContext)
        {
            if (_interactable is not null && _interactable.CanInteractWith() && _interactable.StartInteraction())
                _interactCoroutine = StartCoroutine(Interaction(_interactable.timeOfInteraction));
            
            if (_interactable is TaskObject task && !task.IsFocusTask)
            {
                _thoughtsWidget.ShowTextFor("Me :\nI'll do this later", 2f);
            }
        }

        private void CancelInteract(InputAction.CallbackContext callbackContext)
        {
            StopInteract();
            if (_interactCoroutine != null) StopCoroutine(_interactCoroutine);
        }

        private IEnumerator Interaction(float time)
        {
            if (!interactionSource.isPlaying && _interactable.interactSound is not null)
            {
                interactionSource.PlayOneShot(_interactable.interactSound);
            }
            if (time == 0f)
            {
                _interactable?.Interact();
            }
            else
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
        }

        private void StopInteract()
        {
            interactionSource.Stop();
            _interactable = null;
            _interactWidget.Hide();
            _interactWidget.isInteracting = false;
            if (_interactCoroutine != null) StopCoroutine(_interactCoroutine);
        }
    }
}
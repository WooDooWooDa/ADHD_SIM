using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LightSwitch : MonoBehaviour, IInteractable
    {
        public Light connectedLight;

        private bool _isOn = true;
        private float _baseIntensity;
        
        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }

        public void Start()
        {
            timeOfInteraction = 0f;
            interactionText = "Toggle the light";
            _baseIntensity = connectedLight.intensity;
        }

        public bool StartInteraction()
        {
            return true;
        }

        public void Interact()
        {
            _isOn = !_isOn;
            connectedLight.intensity = _isOn ? _baseIntensity : 0f;
        }

        public bool CanInteractWith()
        {
            return true;
        }
    }
}
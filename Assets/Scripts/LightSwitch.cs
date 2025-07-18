using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class LightSwitch : MonoBehaviour, IInteractable
    {
        public Light connectedLight;

        private bool _isOn = true;
        private float _baseIntensity;
        private AudioSource _audioSource;
        private AudioClip _onClip;
        private AudioClip _offClip;

        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _onClip = Resources.Load<AudioClip>("Sounds/SFX_Flashlight_Click_1");
            _offClip = Resources.Load<AudioClip>("Sounds/SFX_Flashlight_Click_2");
        }

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
            _audioSource.clip = _isOn ? _offClip : _onClip;
            _audioSource.Play();
        }

        public bool CanInteractWith()
        {
            return true;
        }
    }
}
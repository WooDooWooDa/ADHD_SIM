﻿using UnityEngine;

namespace DefaultNamespace
{
    public interface IInteractable
    {
        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }
        public AudioClip interactSound { get; set; }
        public bool StartInteraction();
        public void Interact();
        public bool CanInteractWith();
    }
}
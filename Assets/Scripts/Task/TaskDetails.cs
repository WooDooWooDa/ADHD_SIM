using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public struct TaskDetails
    {
        public string Name;
        public string InteractionText;
        public string Thoughts;
        public float TimeToComplete;
        public float MinutesAdded;
        public AudioClip InteractSound;
    }
}
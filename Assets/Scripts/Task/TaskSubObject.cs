using System;
using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class TaskSubObject : MonoBehaviour, IInteractable
    {
        public bool taskNoticed = false;
        public bool objectFound = false;
        public bool deactivateOnFound = false;
        private Action _subObjectFound;
        private FindObjectsTask _task;
        private TaskList _list;
        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }
        public AudioClip interactSound { get; set; }

        private void Start()
        {
            timeOfInteraction = 0.5f;
        }

        public bool StartInteraction()
        {
            return true;
        }

        public void Interact()
        {
            objectFound = true;
            if (deactivateOnFound)
                gameObject.SetActive(false);
            
            _subObjectFound?.Invoke();
        }

        public bool CanInteractWith()
        {
            return taskNoticed && !objectFound && _list.IfFocusTask(_task);
        }

        public void Register(FindObjectsTask findObjectsTask)
        {
            _task = findObjectsTask;
            interactionText = _task.interactionText;
            _list = FindFirstObjectByType<TaskList>();
            _subObjectFound += findObjectsTask.SubObjectFound;
        }
    }
}
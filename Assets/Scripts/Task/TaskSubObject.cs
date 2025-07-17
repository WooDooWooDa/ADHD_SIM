using System;
using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class TaskSubObject : MonoBehaviour, IInteractable
    {
        public bool taskNoticed = false;
        public bool objectFound = false;
        private Action _subObjectFound;
        private FindObjectsTask _task;
        private TaskList _list;
        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }

        private void Start()
        {
            timeOfInteraction = 1f;
            interactionText = "Did I find the thing?";
        }

        public bool StartInteraction()
        {
            return true;
        }

        public void Interact()
        {
            objectFound = true;
            _subObjectFound?.Invoke();
        }

        public bool CanInteractWith()
        {
            return taskNoticed && !objectFound && _list.IfFocusTask(_task);
        }

        public void Register(FindObjectsTask findObjectsTask)
        {
            _task = findObjectsTask;
            _list = FindFirstObjectByType<TaskList>();
            _subObjectFound += findObjectsTask.SubObjectFound;
        }
    }
}
using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TaskObject : MonoBehaviour, IInteractable
    {
        public TaskDetails Details;
        public TaskType taskType;
        public TaskPriority taskPriority;
        public float timeOfInteraction { get; set; }

        public TaskState taskState
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(value);
            }
        }
        private TaskState _state;
        public Action<TaskState> OnStateChanged;
        public Action<TaskObject> OnDone;
        
        private float _noticedTime;
        private TaskList _list;

        private void Awake()
        {
            _list = FindFirstObjectByType<TaskList>();
            timeOfInteraction = Details.TimeToComplete;
        }

        public void TryNotice()
        {
            if (_state is not TaskState.UnNoticed) return;
            
            _noticedTime += Time.deltaTime;

            if (!TaskHelper.IsNoticedYet(_noticedTime, taskPriority)) return;
            
            //Noticed
            taskState = TaskState.Noticed;
            OnStateChanged?.Invoke(taskState);
            _list.AddTaskToDo(this);
        }

        public void Complete()
        {
            if (_state is not (TaskState.UnDone or TaskState.OnGoing)) return;

            taskState = TaskState.Done;
            taskPriority =  TaskPriority.Done;
            OnDone?.Invoke(this);
            var parts = Instantiate(Resources.Load<ParticleSystem>("ParticlesSystem/TaskCompleteParticleSystem"), transform);
            parts.Play();
            Destroy(parts.gameObject, 5);
        }

        public bool StartInteraction()
        {
            if (_state is not (TaskState.UnDone or TaskState.OnGoing)) return false;
            
            return true;
        }

        public void Interact()
        {
            //basic task just completes it when interacted
            Complete();
        }

        public bool CanInteractWith()
        {
            return _state is (TaskState.UnDone or TaskState.OnGoing) && _list.IfFocusTask(this);
        }
    }
}
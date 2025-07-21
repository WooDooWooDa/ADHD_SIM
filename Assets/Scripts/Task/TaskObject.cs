using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class TaskObject : MonoBehaviour, IInteractable
    {
        public TaskDetails Details;
        public TaskType taskType;
        public TaskPriority taskPriority;
        public float timeOfInteraction { get; set; }
        public string interactionText { get; set; }
        public AudioClip interactSound { get; set; }

        public TaskState taskState
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(value);
            }
        }
        public bool IsFocusTask => list.IfFocusTask(this);
        private TaskState _state;
        public Action<TaskState> OnStateChanged;
        public Action<TaskObject> OnDone;
        
        private float _noticedTime;
        protected TaskList list;

        private void Awake()
        {
            list = FindFirstObjectByType<TaskList>();
            timeOfInteraction = Details.TimeToComplete;
            interactSound = Details.InteractSound;
            interactionText = string.Empty == Details.InteractionText ? "Let's do this now" : Details.InteractionText;
        }

        public virtual void TryNotice()
        {
            if (_state is not TaskState.UnNoticed) return;
            
            _noticedTime += Time.deltaTime;

            if (!TaskHelper.IsNoticedYet(_noticedTime, taskPriority)) return;
            
            //Noticed
            Notice();
        }

        public void Complete()
        {
            if (_state is not (TaskState.UnDone or TaskState.OnGoing)) return;

            taskState = TaskState.Done;
            taskPriority =  TaskPriority.Done;
            OnDone?.Invoke(this);
            Done();
        }

        public bool StartInteraction()
        {
            if (_state is not TaskState.UnDone) return false;
            
            return true;
        }

        public virtual void Interact() { }

        public virtual bool CanInteractWith()
        {
            return _state is (TaskState.UnDone) && IsFocusTask;
        }

        protected virtual void Done()
        {
            //Particles
            var parts = Instantiate(Resources.Load<ParticleSystem>("ParticlesSystem/TaskCompleteParticleSystem"), transform);
            parts.Play();
            Destroy(parts.gameObject, 5);
            
            //Add time
            FindFirstObjectByType<TimeManager>().AddMinutes(Details.MinutesAdded);
            
            //Play sound
            GetComponent<AudioSource>().Play();
        }

        protected virtual void Notice()
        {
            taskState = TaskState.Noticed;
            OnStateChanged?.Invoke(taskState);
            list.AddTaskToDo(this);
            
            //outline
            var outline = GetComponentInParent<Outline>();
            if (outline)
                LeanTween.value(gameObject, (float v) => outline.OutlineWidth = v, 0f, 20f, 0.5f).setLoopPingPong(1);
            
            //Thoughts
            var widget = FindFirstObjectByType<ThoughtsWidget>(FindObjectsInactive.Include);
            widget.ShowTextFor("Me :\n" + Details.Thoughts, 5f);
        } 
    }
}
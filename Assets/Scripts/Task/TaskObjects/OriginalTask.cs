using System;

namespace DefaultNamespace
{
    public class OriginalTask : TaskObject
    {
        private void Start()
        {
            var list = FindFirstObjectByType<TaskList>();
            list.AddTaskToDo(this);
            taskState = TaskState.OnGoing;
        }
    }
}
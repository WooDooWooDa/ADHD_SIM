using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TaskList : MonoBehaviour
    {
        public List<Task> Tasks = new();

        public void AddTask(TaskObject taskObject)
        {
            //add to list
            var newTask = new Task
            {
                linkedTaskObject = taskObject
            };
            Tasks.Insert(GetIndexOfPriority(taskObject), newTask);
            print(Tasks[0]);
            //...
            //mark as "ready to be done"
            taskObject.taskState = TaskState.UnDone;
        }

        private int GetIndexOfPriority(TaskObject taskObject)
        {
            var insertIndex = 0;
            for (var i = 0; i < Tasks.Count; i++)
            {
                var currentPriority = Tasks[i].linkedTaskObject.taskPriority;

                if (currentPriority > taskObject.taskPriority)
                {
                    // Found lower-priority task — insert before it
                    insertIndex = i;
                    break;
                }
                else if (currentPriority == taskObject.taskPriority)
                {
                    // Found same-priority task — insert here (before it)
                    insertIndex = i;
                    break;
                }

                // Otherwise, keep looking
                insertIndex = i + 1;
            }
            return insertIndex;
        }
    }
}
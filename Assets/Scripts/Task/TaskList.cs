using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TaskList : MonoBehaviour
    {
        public List<TaskObject> ToDoTasks = new();
        public Action<List<TaskObject>> OnListUpdated;

        public void AddTaskToDo(TaskObject taskObject)
        {
            //add to list
            ToDoTasks.Insert(GetIndexOfPriority(taskObject), taskObject);
            //mark as "ready to be done"
            taskObject.taskState = TaskState.UnDone;
            taskObject.OnDone += OnTaskDone;
            
            OnListUpdated?.Invoke(ToDoTasks);
        }

        public bool IfFocusTask(TaskObject taskObject)
        {
            return ToDoTasks[0] == taskObject;
        }

        private void OnTaskDone(TaskObject taskObject)
        {
            //place it at the end of the list
            ToDoTasks.Remove(taskObject);
            ToDoTasks.Add(taskObject);
            OnListUpdated?.Invoke(ToDoTasks);
        }

        private int GetIndexOfPriority(TaskObject taskObject)
        {
            var insertIndex = 0;
            for (var i = 0; i < ToDoTasks.Count; i++)
            {
                var currentPriority = ToDoTasks[i].taskPriority;

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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Type
{
    public class FindObjectsTask : TaskObject
    {
        public List<TaskSubObject> SubObjects = new();

        private void Start()
        {
            RegisterSubTask();
        }

        public override void TryNotice()
        {
            base.TryNotice();
            if (taskState == TaskState.UnDone)
            {
                taskState = TaskState.OnGoing;
            }
            
            foreach (var subObject in SubObjects)
            {
                subObject.taskNoticed = true;
            }
        }

        public void SubObjectFound()
        {
            list.OnListUpdated(list.ToDoTasks);
            foreach (var subObject in SubObjects)
            {
                if (!subObject.objectFound) return;
            }
            //Complete task when all subobject found
            Complete();
        }

        private void RegisterSubTask()
        {
            foreach (var subObject in SubObjects)
            {
                subObject.Register(this);
            }
        }
    }
}
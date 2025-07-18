using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Widgets;
using UnityEngine;

public class TaskListWidget : Widget
{
    public TaskListItemWidget ListItemPrefab;
    public Transform ListParentTransform;
    
    private TaskList _list;
    
    void Awake()
    {
        _list = FindFirstObjectByType<TaskList>();
        _list.OnListUpdated += OnListUpdated;
    }

    private void OnListUpdated(List<TaskObject> list)
    {
        //ShowFor(5f);  //Annoying not to know whats next
        for (var i = 0; i < ListParentTransform.childCount; i++) {
            Destroy(ListParentTransform.GetChild(i).gameObject);
        }

        int j = 1;
        foreach (var taskObject in list)
        {
            var task = Instantiate(ListItemPrefab, ListParentTransform);
            task.Init(taskObject, j);
            j++;
        }
    }
}

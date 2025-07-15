using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TaskListWidget : MonoBehaviour
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
        for (var i = 0; i < ListParentTransform.childCount; i++) {
            Destroy(ListParentTransform.GetChild(i).gameObject);
        }

        var first = true;
        foreach (var taskObject in list)
        {
            var task = Instantiate(ListItemPrefab, ListParentTransform);
            task.Init(taskObject, first);
            first = false;
        }
        
    }
}

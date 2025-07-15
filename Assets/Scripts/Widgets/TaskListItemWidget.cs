using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItemWidget : MonoBehaviour
{
    public TextMeshProUGUI taskName;
    public Image unCheckIcon;
    public TextMeshProUGUI taskName2;
    public Transform unDoneTransform;
    public Transform doneTransform;

    public TaskObject TaskObject;

    public void Init(TaskObject taskObject, bool first)
    {
        TaskObject = taskObject;
        taskName.text = taskObject.Details.Name;
        if (!first)
        {
            //set as not first/focus task
        }
        taskName2.text = taskObject.Details.Name;
        taskObject.OnStateChanged += OnTaskStateChanged;
    }

    private void OnTaskStateChanged(TaskState state)
    {
        if (state == TaskState.Done)
        {
            unDoneTransform.gameObject.SetActive(false);
            doneTransform.gameObject.SetActive(true);
        }
    }
}

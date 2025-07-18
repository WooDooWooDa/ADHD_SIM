using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItemWidget : MonoBehaviour
{
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI taskName2;
    public Transform unDoneTransform;
    public Transform doneTransform;
    public TextMeshProUGUI taskNumberText;

    public TaskObject TaskObject;

    public void Init(TaskObject taskObject, int position)
    {
        TaskObject = taskObject;
        taskName.text = taskObject.Details.Name;
        taskNumberText.text = position + ".";
        if (position == 1)
            taskNumberText.fontSize = 64;
        taskName2.text = taskObject.Details.Name;
        if (taskObject.taskState == TaskState.Done)
        {
            doneTransform.gameObject.SetActive(true);
            unDoneTransform.gameObject.SetActive(false);
        }
    }
}

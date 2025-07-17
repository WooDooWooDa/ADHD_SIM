using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class DoDishesTask : HoldToCompleteTask
    {
        protected override void Done()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
            GetComponent<BoxCollider>().enabled = false;
            base.Done();
        }
    }
}
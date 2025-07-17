using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class CleanDinnerTableTask : FindObjectsTask
    {
        protected override void Notice()
        {
            base.Notice();
            GetComponent<BoxCollider>().enabled = false;
        }

        protected override void Done()
        {
            base.Done();
            FindFirstObjectByType<DoDishesTask>(FindObjectsInactive.Include).gameObject.SetActive(true);
        }
    }
}
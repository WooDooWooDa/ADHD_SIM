using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class WasherTask : HoldToCompleteTask
    {
        protected override void Done()
        {
            GetComponentInParent<Animator>().Play("CloseWasher");
            base.Done();
        }
    }
}
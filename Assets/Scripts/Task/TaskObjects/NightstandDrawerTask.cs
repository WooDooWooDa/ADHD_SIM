using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class NightstandDrawerTask : HoldToCompleteTask
    {
        protected override void Done()
        {
            GetComponent<Animator>().Play("CloseNighstandDrawer");
            base.Done();
        }
    }
}
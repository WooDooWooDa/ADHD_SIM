using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class ACTask : FindObjectsTask
    {
        protected override void Done()
        {
            GetComponent<Animator>().Play("AC_on");
            base.Done();
        }
    }
}
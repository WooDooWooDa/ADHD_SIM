using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class MakeTeaTask : StepsTask
    {
        protected override void Done()
        {
            base.Done();
            gameObject.SetActive(false);
        }
    }
}
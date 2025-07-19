using System;
using DefaultNamespace.Type;

namespace DefaultNamespace
{
    public class OriginalTask : FindObjectsTask
    {
        protected override void Done()
        {
            transform.position = FindFirstObjectByType<PlayerController>().transform.position;
            base.Done();
            
            FindFirstObjectByType<Girlfriend>().EndDay();
        }
    }
}
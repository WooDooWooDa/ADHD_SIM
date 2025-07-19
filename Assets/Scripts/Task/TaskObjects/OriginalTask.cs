using System;
using DefaultNamespace.Type;

namespace DefaultNamespace
{
    public class OriginalTask : FindObjectsTask
    {
        protected override void Done()
        {
            base.Done();
            
            FindFirstObjectByType<Girlfriend>().EndDay();
        }
    }
}
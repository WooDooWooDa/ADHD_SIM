using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class ACTask : FindObjectsTask
    {
        public AudioSource acOnSource;
        protected override void Done()
        {
            GetComponent<Animator>().Play("AC_on");
            acOnSource.loop = true;
            acOnSource.Play();
            base.Done();
        }
    }
}
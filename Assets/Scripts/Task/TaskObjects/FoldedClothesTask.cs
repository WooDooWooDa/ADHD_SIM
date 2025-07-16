using DefaultNamespace.Type;

namespace DefaultNamespace
{
    public class FoldedClothesTask : StepsTask
    {
        protected override void Done()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
            base.Done();
        }
    }
}
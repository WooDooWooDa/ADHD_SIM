using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class ToiletPaperTask : FindObjectsTask
    {
        public MeshRenderer toiletHoldheRenderer;
        public Material doneToiletRollMaterial;
        
        protected override void Done()
        {
            var newMaterials = toiletHoldheRenderer.materials;
            newMaterials[1] = doneToiletRollMaterial;
            toiletHoldheRenderer.materials = newMaterials;
            base.Done();
        }
    }
}
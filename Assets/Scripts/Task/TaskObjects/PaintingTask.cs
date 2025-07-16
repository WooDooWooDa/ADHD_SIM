using System;
using DefaultNamespace.Type;
using UnityEngine;

namespace DefaultNamespace
{
    public class PaintingTask : HoldToCompleteTask
    {
        public Vector3 donePaintingPosition;
        public Quaternion donePaintingRotation;
        
        protected override void Done()
        {
            transform.localPosition = donePaintingPosition;
            transform.localRotation = donePaintingRotation;
            base.Done();
        }
    }
}
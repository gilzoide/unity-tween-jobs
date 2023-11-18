using System;
using UnityEngine;

namespace Gilzoide.TweenJobs.Tweeners
{
    [Serializable]
    public class EulerAnglesTweener : AVector3Tweener
    {
        public Transform Target;

        public override Vector3 Value
        {
            get => Target.eulerAngles;
            set => Target.eulerAngles = value;
        }
    }

    public class EulerAnglesTween : ATweenComponent<EulerAnglesTweener>
    {
    }
}

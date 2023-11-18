using System;
using UnityEngine;

namespace Gilzoide.TweenJobs.Tweeners
{
    [Serializable]
    public class LocalEulerAnglesTweener : AVector3Tweener
    {
        public Transform Target;

        public override Vector3 Value
        {
            get => Target.localEulerAngles;
            set => Target.localEulerAngles = value;
        }
    }

    public class LocalEulerAnglesTween : ATweenComponent<LocalEulerAnglesTweener>
    {
    }
}

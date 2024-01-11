using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class RotationTweener : AQuaternionTweener
    {
        public Transform Target;

        public override Quaternion Value
        {
            get => Target.rotation;
            set => Target.rotation = value;
        }
    }

    public class RotationTween : ATweenComponent<RotationTweener>
    {
    }
}

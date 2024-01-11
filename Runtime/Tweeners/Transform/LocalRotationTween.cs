using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class LocalRotationTweener : AQuaternionTweener
    {
        public Transform Target;

        public override Quaternion Value
        {
            get => Target.localRotation;
            set => Target.localRotation = value;
        }
    }

    public class LocalRotationTween : ATweenComponent<LocalRotationTweener>
    {
    }
}

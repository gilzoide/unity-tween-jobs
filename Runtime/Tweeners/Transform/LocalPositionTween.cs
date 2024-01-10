using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class LocalPositionTweener : AVector3Tweener
    {
        public Transform Target;

        public override Vector3 Value
        {
            get => Target.localPosition;
            set => Target.localPosition = value;
        }
    }

    public class LocalPositionTween : ATweenComponent<LocalPositionTweener>
    {
    }
}

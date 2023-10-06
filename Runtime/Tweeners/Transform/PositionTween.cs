using System;
using UnityEngine;

namespace Gilzoide.TweenJobs.Tweeners
{
    [Serializable]
    public class PositionTweener : AVector3Tweener
    {
        public Transform Target;

        public override Vector3 Value
        {
            get => Target.position;
            set => Target.position = value;
        }
    }

    public class PositionTween : ATweenComponent<PositionTweener>
    {
    }
}

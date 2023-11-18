using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class LocalScaleTweener : AVector3Tweener
    {
        public Transform Target;

        public override Vector3 Value
        {
            get => Target.localScale;
            set => Target.localScale = value;
        }
    }

    public class LocalScaleTween : ATweenComponent<LocalScaleTweener>
    {
    }
}

using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class Vector3Tweener : AVector3Tweener
    {
        public Func<Vector3> GetValue;
        public Action<Vector3> SetValue;

        public Vector3Tweener()
        {
        }

        public Vector3Tweener(Func<Vector3> getValue, Action<Vector3> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Vector3 Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class Vector3Tween : ATweenComponent<Vector3Tweener>
    {
    }
}

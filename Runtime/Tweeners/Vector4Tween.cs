using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class Vector4Tweener : AVector4Tweener
    {
        public Func<Vector4> GetValue;
        public Action<Vector4> SetValue;

        public Vector4Tweener()
        {
        }

        public Vector4Tweener(Func<Vector4> getValue, Action<Vector4> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Vector4 Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class Vector4Tween : ATweenComponent<Vector4Tweener>
    {
    }
}

using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class Vector2Tweener : AVector2Tweener
    {
        public Func<Vector2> GetValue;
        public Action<Vector2> SetValue;

        public Vector2Tweener(Func<Vector2> getValue, Action<Vector2> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Vector2 Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class Vector2Tween : ATweenComponent<Vector2Tweener>
    {
    }
}

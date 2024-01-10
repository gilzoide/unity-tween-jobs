using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class RectTweener : ARectTweener
    {
        public Func<Rect> GetValue;
        public Action<Rect> SetValue;

        public RectTweener()
        {
        }

        public RectTweener(Func<Rect> getValue, Action<Rect> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Rect Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class RectTween : ATweenComponent<RectTweener>
    {
    }
}

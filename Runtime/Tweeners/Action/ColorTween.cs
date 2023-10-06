using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class ColorTweener : AColorTweener
    {
        public Func<Color> GetValue;
        public Action<Color> SetValue;

        public ColorTweener()
        {
        }

        public ColorTweener(Func<Color> getValue, Action<Color> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Color Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class ColorTween : ATweenComponent<ColorTweener>
    {
    }
}

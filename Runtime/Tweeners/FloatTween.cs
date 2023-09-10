using System;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class FloatTweener : AFloatTweener
    {
        public Func<float> GetValue;
        public Action<float> SetValue;

        public FloatTweener(Func<float> getValue, Action<float> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override float Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class FloatTween : ATweenComponent<FloatTweener>
    {
    }
}

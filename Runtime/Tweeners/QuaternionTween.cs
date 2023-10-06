using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public class QuaternionTweener : AQuaternionTweener
    {
        public Func<Quaternion> GetValue;
        public Action<Quaternion> SetValue;

        public QuaternionTweener(Func<Quaternion> getValue, Action<Quaternion> setValue)
        {
            GetValue = getValue;
            SetValue = setValue;
        }

        public override Quaternion Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }

    public class QuaternionTween : ATweenComponent<QuaternionTweener>
    {
    }
}

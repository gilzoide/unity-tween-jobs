using System.Collections;
using System.Collections.Generic;
using Gilzoide.UpdateManager.Jobs;
using Unity.Burst;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public struct TweenJob<T, TInterpolator> : IUpdateJob
        where T : struct
        where TInterpolator : struct, IInterpolator<T>
    {
        public T From;
        public T To;
        public float Duration;
        public float TimeScale;
        public float Time { get; private set; }
        public float Progress => _easingFunctionPointer.Invoke(Time / Duration);
        public T Value => Interpolator.Interpolate(From, To, Progress);
        public bool IsComplete => Time >= Duration;
        public TInterpolator Interpolator;
        public Easings.Functions EasingFunction
        {
            get => _easingFunction;
            set
            {
                _easingFunction = value;
                _easingFunctionPointer = Easings.GetFunctionPointer(value);
            }
        }

        private Easings.Functions _easingFunction;
        private FunctionPointer<Easings.EasingFunctionDelegate> _easingFunctionPointer;

        public void Execute()
        {
            Time += TimeScale * UpdateJobTime.deltaTime;
        }
    }
}

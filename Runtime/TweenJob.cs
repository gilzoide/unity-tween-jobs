using System;
using Gilzoide.UpdateManager.Jobs;
using Unity.Burst;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public struct TweenJob<T, TValueMath> : IUpdateJob
        where T : struct
        where TValueMath : struct, IValueMath<T>
    {
        public T From;
        public T To;
        public float Duration;
        public float TimeScale;
        public FunctionPointer<Easings.EasingFunctionDelegate> EasingFunctionPointer;
        public float Time { get; set; }
        public float Progress { get; private set; }
        public T Value { get; private set; }
        public bool IsComplete => Time >= Duration;

        private readonly TValueMath _valueMath;

        public void Execute()
        {
            Time += TimeScale * UpdateJobTime.deltaTime;
            Progress = EasingFunctionPointer.Invoke(Time / Duration);
            Value = _valueMath.Interpolate(From, To, Progress);
        }
    }
}

using Gilzoide.TweenJobs.Math;
using Gilzoide.UpdateManager.Jobs;
using Unity.Burst;
using Unity.Mathematics;

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
        public bool UseUnscaledDeltaTime;
        public FunctionPointer<Easings.EasingFunctionDelegate> EasingFunctionPointer;
        public float Time { get; set; }
        public float Progress { get; private set; }
        public T Value { get; private set; }
        public bool IsComplete => Time >= Duration;

        private readonly TValueMath _valueMath;

        public void Execute()
        {
            float deltaTime = UseUnscaledDeltaTime ? UpdateJobTime.unscaledDeltaTime : UpdateJobTime.deltaTime;
            Time += TimeScale * deltaTime;
            Progress = math.clamp(EasingFunctionPointer.Invoke(Time / Duration), 0, 1);
            Value = _valueMath.Interpolate(From, To, Progress);
        }
    }
}

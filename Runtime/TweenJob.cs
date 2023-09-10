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
        public static readonly TValueMath ValueMath;

        public T From;
        public T To;
        public float Duration;
        public float Speed;
        public bool UseUnscaledDeltaTime;
        public FunctionPointer<Easings.EasingFunctionDelegate> EasingFunctionPointer;
        public int LoopCount;
        public LoopType LoopType;
        public float Time { get; set; }
        public float Progress { get; private set; }
        public T Value { get; private set; }
        public int LoopIndex { get; private set; }
        public bool IsComplete { get; private set; }

        public void Execute()
        {
            float deltaTime = UseUnscaledDeltaTime ? UpdateJobTime.unscaledDeltaTime : UpdateJobTime.deltaTime;
            Time += Speed * deltaTime;
            LoopIndex = (int) (math.abs(Time) / Duration);
            if (LoopCount >= 0 && LoopIndex > LoopCount)
            {
                Progress = Speed >= 0 ? 1 : 0;
                Value = Speed >= 0 ? To : From;
                IsComplete = true;
            }
            else
            {
                float time = LoopType.LoopValue(Time, Duration);
                Progress = EasingFunctionPointer.Invoke(time / Duration);
                Value = ValueMath.Interpolate(From, To, Progress);
                IsComplete = false;
            }
        }
    }
}

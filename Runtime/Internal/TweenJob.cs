using Gilzoide.TweenJobs.Math;
using Gilzoide.UpdateManager.Jobs;
using Unity.Burst;
using Unity.Mathematics;

namespace Gilzoide.TweenJobs.Internal
{
    public struct TweenJob<T, TValueMath> : IBurstUpdateJob<BurstUpdateJob<TweenJob<T, TValueMath>>>
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

        public float FinalProgress
        {
            get
            {
                if (LoopType == LoopType.PingPong && LoopCount > 0)
                {
                    if (LoopCount % 2 == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (Speed >= 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Execute()
        {
            float deltaTime = UseUnscaledDeltaTime ? UpdateJobTime.unscaledDeltaTime : UpdateJobTime.deltaTime;
            Time += Speed * deltaTime;
            LoopIndex = (int) (math.abs(Time) / Duration);
            if (LoopCount >= 0 && LoopIndex > LoopCount)
            {
                Progress = FinalProgress;
                IsComplete = true;
            }
            else
            {
                float time = LoopType.LoopValue(Time, Duration);
                Progress = EasingFunctionPointer.Invoke(time / Duration);
                IsComplete = false;
            }
            Value = ValueMath.Interpolate(From, To, Progress);
        }
    }
}

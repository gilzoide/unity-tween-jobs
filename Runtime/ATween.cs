using System;
using Gilzoide.UpdateManager.Jobs;
using Gilzoide.UpdateManager.Jobs.Internal;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public abstract class ATween<T, TInterpolator, TJob> : IJobUpdatable<TweenJob<T, TInterpolator>, TJob>,
        IJobDataSynchronizer<TweenJob<T, TInterpolator>>
        where T : struct
        where TInterpolator : struct, IInterpolator<T>
        where TJob : struct, IInternalUpdateJob<TweenJob<T, TInterpolator>>
    {
        public T From;
        public T To;
        public float Duration = 1;
        public float TimeScale = 1;
        public Easings.Functions EasingFunction = Easings.Functions.Linear;
        public event Action OnComplete;

        public abstract T Value { get; set; }

        public TweenJob<T, TInterpolator> InitialJobData => new TweenJob<T, TInterpolator>
        {
            From = From,
            To = To,
            Duration = Duration,
            TimeScale = TimeScale,
            EasingFunction = EasingFunction,
        };

        public void Start()
        {
            this.RegisterInManager(true);
        }

        public void SyncJobData(ref TweenJob<T, TInterpolator> jobData)
        {
            if (jobData.IsComplete)
            {
                this.UnregisterInManager();
                OnComplete?.Invoke();
            }
            else
            {
                jobData = InitialJobData;
            }
        }
    }

    public abstract class ATweenFloat : ATween<float, FloatInterpolator, BurstUpdateJob<TweenJob<float, FloatInterpolator>>>
    {
    }

    public abstract class ATweenVector2 : ATween<Vector2, Vector2Interpolator, BurstUpdateJob<TweenJob<Vector2, Vector2Interpolator>>>
    {
    }

    public abstract class ATweenVector3 : ATween<Vector3, Vector3Interpolator, BurstUpdateJob<TweenJob<Vector3, Vector3Interpolator>>>
    {
    }

    public abstract class ATweenVector4 : ATween<Vector4, Vector4Interpolator, BurstUpdateJob<TweenJob<Vector4, Vector4Interpolator>>>
    {
    }

    public abstract class ATweenQuaternion : ATween<Quaternion, QuaternionInterpolator, BurstUpdateJob<TweenJob<Quaternion, QuaternionInterpolator>>>
    {
    }

    public abstract class ATweenColor : ATween<Color, ColorInterpolator, BurstUpdateJob<TweenJob<Color, ColorInterpolator>>>
    {
    }

    public abstract class ATweenColor32 : ATween<Color32, Color32Interpolator, BurstUpdateJob<TweenJob<Color32, Color32Interpolator>>>
    {
    }
}

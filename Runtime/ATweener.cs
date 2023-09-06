using System;
using Gilzoide.UpdateManager.Jobs;
using Gilzoide.UpdateManager.Jobs.Internal;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public abstract class ATweener<T, TValueMath, TJob> : IJobUpdatable<TweenJob<T, TValueMath>, TJob>,
        IJobDataSynchronizer<TweenJob<T, TValueMath>>,
        ITweener
        where T : struct, IEquatable<T>
        where TValueMath : struct, IValueMath<T>
        where TJob : struct, IInternalUpdateJob<TweenJob<T, TValueMath>>
    {
        public T From
        {
            get => _from;
            set
            {
                if (!_from.Equals(value))
                {
                    _from = value;
                    _isDirty = true;
                }
            }
        }
        public T To
        {
            get => _to;
            set
            {
                if (!_to.Equals(value))
                {
                    _to = value;
                    _isDirty = true;
                }
            }
        }
        public bool IsRelative
        {
            get => _isRelative;
            set
            {
                if (_isRelative != value)
                {
                    _isRelative = value;
                    _isDirty = true;
                }
            }
        }
        public float Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    _isDirty = true;
                }
            }
        }
        public float TimeScale
        {
            get => _timeScale;
            set
            {
                if (_timeScale != value)
                {
                    _timeScale = value;
                    _isDirty = true;
                }
            }
        }
        public float Time
        {
            get => _time ?? this.GetJobData().Time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    _isDirty = true;
                }
            }
        }
        public Easings.Functions EasingFunction
        {
            get => _easingFunction;
            set
            {
                if (_easingFunction != value)
                {
                    _easingFunction = value;
                    _isDirty = true;
                }
            }
        }
        public event Action OnComplete;

        [SerializeField] protected T _from;
        [SerializeField] protected T _to;
        [SerializeField] protected bool _isRelative;
        [SerializeField] protected float _duration = 1;
        [SerializeField] protected float _timeScale = 1;
        [SerializeField] protected Easings.Functions _easingFunction;

        protected bool _isDirty;
        protected T _initialValue;
        protected TValueMath _valueMath;
        protected float? _time;

        public abstract T Value { get; set; }

        public TweenJob<T, TValueMath> InitialJobData => new TweenJob<T, TValueMath>
        {
            From = _isRelative ? _valueMath.Add(_initialValue, _from) : _from,
            To = _isRelative ? _valueMath.Add(_initialValue, _to) : _to,
            Duration = Duration,
            TimeScale = TimeScale,
            Time = _time ?? 0,
            EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction),
        };

        public bool IsPlaying => this.IsRegisteredInManager();

        public void Play()
        {
            _initialValue = Value;
            this.RegisterInManager(true);
        }

        public void Pause()
        {
            this.UnregisterInManager();
        }

        public void Rewind()
        {
            Time = 0;
            Apply();
        }

        public void SyncJobData(ref TweenJob<T, TValueMath> jobData)
        {
            Value = jobData.Value;
            if (jobData.IsComplete)
            {
                Pause();
                OnComplete?.Invoke();
            }
            else
            {
                if (_isDirty)
                {
                    _isDirty = false;
                    jobData.From = _isRelative ? _valueMath.Add(_initialValue, _from) : _from;
                    jobData.To = _isRelative ? _valueMath.Add(_initialValue, _to) : _to;
                    jobData.Duration = Duration;
                    jobData.TimeScale = TimeScale;
                    jobData.EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction);
                    if (_time is float time)
                    {
                        jobData.Time = time;
                    }
                }
            }
        }

        private void Apply()
        {
            if (!IsPlaying)
            {
                var job = InitialJobData;
                job.Execute();
                Value = job.Value;
            }
        }
    }

    public abstract class AFloatTweener : ATweener<float, FloatMath, BurstUpdateJob<TweenJob<float, FloatMath>>>
    {
    }

    public abstract class AVector2Tweener : ATweener<Vector2, Vector2Math, BurstUpdateJob<TweenJob<Vector2, Vector2Math>>>
    {
    }

    public abstract class AVector3Tweener : ATweener<Vector3, Vector3Math, BurstUpdateJob<TweenJob<Vector3, Vector3Math>>>
    {
    }

    public abstract class AVector4Tweener : ATweener<Vector4, Vector4Math, BurstUpdateJob<TweenJob<Vector4, Vector4Math>>>
    {
    }

    public abstract class AQuaternionTweener : ATweener<Quaternion, QuaternionMath, BurstUpdateJob<TweenJob<Quaternion, QuaternionMath>>>
    {
    }

    public abstract class AColorTweener : ATweener<Color, ColorMath, BurstUpdateJob<TweenJob<Color, ColorMath>>>
    {
    }
}

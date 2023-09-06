using System;
using Gilzoide.TweenJobs.Math;
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
        public T ReferenceValue
        {
            get
            {
                if (_referenceValue == null)
                {
                    _referenceValue = Value;
                }
                return _referenceValue.Value;
            }
            set
            {
                if (!_referenceValue.Equals(value))
                {
                    _referenceValue = value;
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
        public float Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
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
        public bool UseUnscaledDeltaTime
        {
            get => _useUnscaledDeltaTime;
            set
            {
                if (_useUnscaledDeltaTime != value)
                {
                    _useUnscaledDeltaTime = value;
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
        [SerializeField, Min(0)] protected float _duration = 1;
        [SerializeField] protected float _speed = 1;
        [SerializeField] public bool _useUnscaledDeltaTime = false;
        [SerializeField] protected Easings.Functions _easingFunction;

        protected bool _isDirty;
        protected T? _referenceValue;
        protected TValueMath _valueMath;
        protected float? _time;

        public abstract T Value { get; set; }

        public TweenJob<T, TValueMath> InitialJobData
        {
            get
            {
                var jobData = new TweenJob<T, TValueMath>
                {
                    From = _isRelative ? _valueMath.Add(ReferenceValue, _from) : _from,
                    To = _isRelative ? _valueMath.Add(ReferenceValue, _to) : _to,
                    Duration = Duration,
                    Speed = Speed,
                    UseUnscaledDeltaTime = UseUnscaledDeltaTime,
                    Time = _time ?? (Speed >= 0 ? 0 : Duration),
                    EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction),
                };
                _time = null;
                _isDirty = false;
                return jobData;
            }
        }

        public bool IsPlaying => this.IsRegisteredInManager();

        public void Play()
        {
            _time = null;
            Unpause();
        }

        public void PlayForward()
        {
            if (Speed < 0)
            {
                Speed = -Speed;
            }
            Play();
        }

        public void PlayBackward()
        {
            if (Speed > 0)
            {
                Speed = -Speed;
            }
            Play();
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                _time = Time;
                this.UnregisterInManager();
            }
        }

        public void Unpause()
        {
            if (Duration > 0)
            {
                this.RegisterInManager(true);
            }
            else
            {
                Complete();
            }
        }

        public void Rewind()
        {
            _time = null;
            this.UnregisterInManager();
            T firstValue = Speed >= 0 ? _from : _to;
            if (IsRelative)
            {
                firstValue = _valueMath.Add(ReferenceValue, firstValue);
            }
            Value = firstValue;
        }

        public void Complete()
        {
            _time = null;
            this.UnregisterInManager();
            T finalValue = Speed >= 0 ? _to : _from;
            if (IsRelative)
            {
                finalValue = _valueMath.Add(ReferenceValue, finalValue);
            }
            Value = finalValue;
        }

        public void SyncJobData(ref TweenJob<T, TValueMath> jobData)
        {
            Value = jobData.Value;
            if (jobData.IsComplete)
            {
                Pause();
                OnComplete?.Invoke();
            }
            else if (_isDirty)
            {
                _isDirty = false;
                jobData.From = _isRelative ? _valueMath.Add(ReferenceValue, _from) : _from;
                jobData.To = _isRelative ? _valueMath.Add(ReferenceValue, _to) : _to;
                jobData.Duration = Duration;
                jobData.Speed = Speed;
                jobData.UseUnscaledDeltaTime = UseUnscaledDeltaTime;
                jobData.EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction);
                if (_time is float time)
                {
                    jobData.Time = time;
                    _time = null;
                }
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

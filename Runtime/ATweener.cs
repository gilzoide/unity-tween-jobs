using System;
using Gilzoide.TweenJobs.Internal;
using Gilzoide.TweenJobs.Math;
using Gilzoide.UpdateManager.Jobs;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    [Serializable]
    public abstract class ATweener<T, TValueMath, TJob> : IJobUpdatable<TweenJob<T, TValueMath>>,
        IJobDataSynchronizer<TweenJob<T, TValueMath>>,
        ITweener,
        IValidatable
        where T : struct, IEquatable<T>
        where TValueMath : struct, IValueMath<T>
    {
        public static readonly TValueMath ValueMath;

        [Tooltip("Initial tween value. If IsRelative is true, an initial reference value is added to this value before used.")]
        [SerializeField] protected T _from;

        [Tooltip("Final tween value. If IsRelative is true, an initial reference value is added to this value before used.")]
        [SerializeField] protected T _to;

        [Tooltip("Whether To and From values are absolute or relative to an initial reference value.")]
        [SerializeField] protected bool _isRelative;

        [Tooltip("Tween duration, in seconds. Zero and negative values make the tween end in the same frame as it starts.")]
        [SerializeField, Min(0)] protected float _duration = 1;

        [Tooltip("Tween speed multiplier. Negative values make the tween run backwards.")]
        [SerializeField] protected float _speed = 1;

        [Tooltip("If true, tweener will use Time.unscaledDeltaTime. Otherwise, use Time.deltaTime.")]
        [SerializeField] protected bool _useUnscaledDeltaTime = false;

        [Tooltip("Easing function used to interpolate values between To and From.")]
        [SerializeField] protected Easings.Functions _easingFunction;

        [Tooltip("How many times the tween will loop before completing. Set to -1 to loop forever.")]
        [SerializeField, Min(-1)] protected int _loopCount;

        [Tooltip("How the values will behave when looping, whether it should restart from the To value or ping-pong between To and From.")]
        [SerializeField] protected LoopType _loopType;

        #region Properties

        /// <summary>
        /// Initial tween value.
        /// If <see cref="IsRelative"/> is true, <see cref="ReferenceValue"/> is added to this value before used.
        /// </summary>
        public T From
        {
            get => _from;
            set
            {
                if (!_from.Equals(value))
                {
                    _from = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Final tween value.
        /// If <see cref="IsRelative"/> is true, <see cref="ReferenceValue"/> is added to this value before used.
        /// </summary>
        public T To
        {
            get => _to;
            set
            {
                if (!_to.Equals(value))
                {
                    _to = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Reference value used if <see cref="IsRelative"/> is true.
        /// This value is automatically set with <see cref="Value"/> when first accessed.
        /// </summary>
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
                if (_referenceValue == null || !_referenceValue.Equals(value))
                {
                    _referenceValue = value;
                    if (_isRelative)
                    {
                        SetDirty();
                    }
                }
            }
        }

        /// <summary>
        /// Whether <see cref="To"/> and <see cref="From"/> values are absolute or relative to <see cref="ReferenceValue"/>.
        /// </summary>
        public bool IsRelative
        {
            get => _isRelative;
            set
            {
                if (_isRelative != value)
                {
                    _isRelative = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Tween duration, in seconds.
        /// Zero and negative values make the tween end in the same frame as it starts.
        /// </summary>
        public float Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Tween speed multiplier.
        /// Negative values make the tween run backwards.
        /// </summary>
        public float Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Current tween time.
        /// Set this to manually advance or rewind a tween.
        /// </summary>
        public float Time
        {
            get => _time ?? this.GetJobData().Time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Whether tweener will use Unity's unscaled delta time.
        /// </summary>
        public bool UseUnscaledDeltaTime
        {
            get => _useUnscaledDeltaTime;
            set
            {
                if (_useUnscaledDeltaTime != value)
                {
                    _useUnscaledDeltaTime = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Easing function used to interpolate values between <see cref="To"/> and <see cref="From"/>.
        /// </summary>
        public Easings.Functions EasingFunction
        {
            get => _easingFunction;
            set
            {
                if (_easingFunction != value)
                {
                    _easingFunction = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// How many times this tween will loop before completing.
        /// Set to -1 to loop forever.
        /// </summary>
        public int LoopCount
        {
            get => _loopCount;
            set
            {
                if (_loopCount != value)
                {
                    _loopCount = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// How the values will behave when looping, whether it should restart from the To value or ping-pong between To and From.
        /// </summary>
        public LoopType LoopType
        {
            get => _loopType;
            set
            {
                if (_loopType != value)
                {
                    _loopType = value;
                    SetDirty();
                }
            }
        }

        #endregion

        public event Action OnComplete;

        private bool _isDirty;
        protected T? _referenceValue;
        protected float? _time;

        /// <summary>
        /// Value being interpolated by the tweener.
        /// Implement this in subclasses to define how to get the current value and what happens when the value changes.
        /// </summary>
        public abstract T Value { get; set; }

        /// <returns>
        /// Whether tweener is playing or not.
        /// </returns>
        public bool IsPlaying => this.IsRegisteredInManager();

        /// <summary>
        /// Returns the initial internal job data.
        /// You should never call this yourself.
        /// </summary>
        public TweenJob<T, TValueMath> InitialJobData
        {
            get
            {
                var jobData = new TweenJob<T, TValueMath>
                {
                    From = _isRelative ? ValueMath.Add(ReferenceValue, _from) : _from,
                    To = _isRelative ? ValueMath.Add(ReferenceValue, _to) : _to,
                    Duration = Duration,
                    Speed = Speed,
                    UseUnscaledDeltaTime = UseUnscaledDeltaTime,
                    Time = _time ?? 0,
                    LoopCount = LoopCount,
                    LoopType = LoopType,
                    EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction),
                };
                _time = null;
                _isDirty = false;
                return jobData;
            }
        }

        /// <summary>
        /// Start playing the tween from the beginning.
        /// </summary>
        /// <remarks>
        /// This method resets <see cref="Time"/> to 0.
        /// Use <see cref="Unpause"/> instead if you don't want <see cref="Time"/> to change.
        /// </remarks>
        public void Play()
        {
            _time = null;
            Unpause();
        }

        /// <summary>
        /// Start playing the tween forward from the beginning.
        /// </summary>
        /// <remarks>
        /// If the current <see cref="Speed"/> is negative, it is reversed to make sure the tween plays forward.
        /// Use <see cref="Play"/> instead if you don't want <see cref="Speed"/> to change.
        /// </remarks>
        public void PlayForward()
        {
            if (Speed < 0)
            {
                Speed = -Speed;
            }
            Play();
        }

        /// <summary>
        /// Start playing the tween backward from the beginning.
        /// </summary>
        /// <remarks>
        /// If the current <see cref="Speed"/> is positive, it is reversed to make sure the tween plays backwards.
        /// Use <see cref="Play"/> instead if you don't want <see cref="Speed"/> to change.
        /// </remarks>
        public void PlayBackward()
        {
            if (Speed > 0)
            {
                Speed = -Speed;
            }
            Play();
        }

        /// <summary>
        /// Pause the tween, if it was playing.
        /// </summary>
        public void Pause()
        {
            if (IsPlaying)
            {
                _time = Time;
                this.UnregisterInManager();
            }
        }

        /// <summary>
        /// Unpause the tween without changing the current <see cref="Time"/>.
        /// </summary>
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

        /// <summary>
        /// Rewind the tween, setting <see cref="Value"/> to its first value.
        /// </summary>
        public void Rewind()
        {
            _time = null;
            this.UnregisterInManager();
            T firstValue = Speed >= 0 ? _from : _to;
            if (IsRelative)
            {
                firstValue = ValueMath.Add(ReferenceValue, firstValue);
            }
            Value = firstValue;
        }

        /// <summary>
        /// Complete the tween, setting <see cref="Value"/> to the final value.
        /// </summary>
        public void Complete()
        {
            _time = null;
            this.UnregisterInManager();
            T finalValue = Speed >= 0 ? _to : _from;
            if (IsRelative)
            {
                finalValue = ValueMath.Add(ReferenceValue, finalValue);
            }
            Value = finalValue;
        }

        /// <summary>
        /// Synchronizes internal job data.
        /// You should never call this yourself.
        /// </summary>
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
                jobData.From = _isRelative ? ValueMath.Add(ReferenceValue, _from) : _from;
                jobData.To = _isRelative ? ValueMath.Add(ReferenceValue, _to) : _to;
                jobData.Duration = Duration;
                jobData.Speed = Speed;
                jobData.UseUnscaledDeltaTime = UseUnscaledDeltaTime;
                jobData.EasingFunctionPointer = Easings.GetFunctionPointer(_easingFunction);
                jobData.LoopCount = LoopCount;
                jobData.LoopType = LoopType;
                if (_time is float time)
                {
                    jobData.Time = time;
                    _time = null;
                }
            }
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            SetDirty();
        }
#endif

        protected internal void SetDirty()
        {
            _isDirty = true;
        }
    }

    public abstract class AFloatTweener : ATweener<float, FloatMath, BurstUpdateJob<TweenJob<float, FloatMath>>> {}
    public abstract class AVector2Tweener : ATweener<Vector2, Vector2Math, BurstUpdateJob<TweenJob<Vector2, Vector2Math>>> {}
    public abstract class AVector3Tweener : ATweener<Vector3, Vector3Math, BurstUpdateJob<TweenJob<Vector3, Vector3Math>>> {}
    public abstract class AVector4Tweener : ATweener<Vector4, Vector4Math, BurstUpdateJob<TweenJob<Vector4, Vector4Math>>> {}
    public abstract class AQuaternionTweener : ATweener<Quaternion, QuaternionMath, BurstUpdateJob<TweenJob<Quaternion, QuaternionMath>>> {}
    public abstract class AColorTweener : ATweener<Color, ColorMath, BurstUpdateJob<TweenJob<Color, ColorMath>>> {}
    public abstract class ARectTweener : ATweener<Rect, RectMath, BurstUpdateJob<TweenJob<Rect, RectMath>>> {}
}

using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public abstract class ATweenComponent : MonoBehaviour, ITweener
    {
        public bool PlayOnEnable = true;

        protected virtual void OnEnable()
        {
            if (PlayOnEnable)
            {
                Play();
            }
        }

        protected virtual void OnDestroy()
        {
            Pause();
        }

        public abstract bool IsPlaying { get; }
        public abstract void Play();
        public abstract void PlayForward();
        public abstract void PlayBackward();
        public abstract void Pause();
        public abstract void Rewind();
    }

    public abstract class ATweenComponent<TTweener> : ATweenComponent
        where TTweener : ITweener
    {
        public TTweener Tweener;

        public override bool IsPlaying => Tweener.IsPlaying;

        public override void Play()
        {
            Tweener.Play();
        }
        public override void PlayForward()
        {
            Tweener.PlayForward();
        }
        public override void PlayBackward()
        {
            Tweener.PlayBackward();
        }
        public override void Pause()
        {
            Tweener.Pause();
        }
        public override void Rewind()
        {
            Tweener.Rewind();
        }
    }
}

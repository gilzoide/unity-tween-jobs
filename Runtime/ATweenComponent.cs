using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public abstract class ATweenComponent : MonoBehaviour, ITweener
    {
        public TweenCommand ActionOnEnable = TweenCommand.Unpause;
        public TweenCommand ActionOnDisable = TweenCommand.Pause;

        protected virtual void OnEnable()
        {
            this.ExecuteCommand(ActionOnEnable);
        }

        protected virtual void OnDisable()
        {
            this.ExecuteCommand(ActionOnDisable);
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
        public abstract void Unpause();
        public abstract void Complete();
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
        public override void Unpause()
        {
            Tweener.Unpause();
        }
         public override void Complete()
        {
            Tweener.Complete();
        }
        public override void Rewind()
        {
            Tweener.Rewind();
        }
    }
}

using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public abstract class ATweenComponent : MonoBehaviour, ITweener
    {
        public TweenCommand ActionOnEnable = TweenCommand.Unpause;
        public TweenCommand ActionOnDisable = TweenCommand.Pause;

        public abstract ITweener GetTweener();

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

        public bool IsPlaying => GetTweener().IsPlaying;

        [ContextMenu("Play")]
        public void Play()
        {
            GetTweener().Play();
        }

        [ContextMenu("Play Forward")]
        public void PlayForward()
        {
            GetTweener().PlayForward();
        }

        [ContextMenu("Play Backward")]
        public void PlayBackward()
        {
            GetTweener().PlayBackward();
        }

        [ContextMenu("Pause")]
        public void Pause()
        {
            GetTweener().Pause();
        }

        [ContextMenu("Unpause")]
        public void Unpause()
        {
            GetTweener().Unpause();
        }

        [ContextMenu("Complete")]
        public void Complete()
        {
            GetTweener().Complete();
        }

        [ContextMenu("Rewind")]
        public void Rewind()
        {
            GetTweener().Rewind();
        }

#if UNITY_EDITOR
        [ContextMenu("Play", isValidateFunction: true)]
        [ContextMenu("Play Forward", isValidateFunction: true)]
        [ContextMenu("Play Backward", isValidateFunction: true)]
        [ContextMenu("Pause", isValidateFunction: true)]
        [ContextMenu("Unpause", isValidateFunction: true)]
        [ContextMenu("Complete", isValidateFunction: true)]
        [ContextMenu("Rewind", isValidateFunction: true)]
        private bool ValidateContextMenuItems()
        {
            return Application.isPlaying;
        }
#endif
    }

    public abstract class ATweenComponent<TTweener> : ATweenComponent
        where TTweener : ITweener
    {
        public TTweener Tweener;

        public override ITweener GetTweener() => Tweener;
    }
}

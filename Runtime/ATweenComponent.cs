using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public abstract class ATweenComponent : MonoBehaviour, ITweener
    {
        public bool PlayOnStart = true;

        void Start()
        {
            if (PlayOnStart)
            {
                Play();
            }
        }

        public abstract bool IsPlaying { get; }
        public abstract void Play();
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
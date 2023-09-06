namespace Gilzoide.TweenJobs
{
    public interface ITweener
    {
        bool IsPlaying { get; }
        void Play();
        void PlayForward();
        void PlayBackward();
        void Pause();
        void Rewind();
    }
}

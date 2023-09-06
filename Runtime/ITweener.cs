namespace Gilzoide.TweenJobs
{
    public interface ITweener
    {
        bool IsPlaying { get; }
        void Play();
        void PlayForward();
        void PlayBackward();
        void Pause();
        void Unpause();
        void Complete();
        void Rewind();
    }
}

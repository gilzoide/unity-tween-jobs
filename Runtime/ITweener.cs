namespace Gilzoide.TweenJobs
{
    public interface ITweener
    {
        bool IsPlaying { get; }
        void Play();
        void Pause();
        void Rewind();
    }
}

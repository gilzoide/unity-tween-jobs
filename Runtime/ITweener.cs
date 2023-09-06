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

    public static class ITweenerExtensions
    {
        public static void ExecuteCommand(this ITweener tweener, TweenCommand command)
        {
            switch (command)
            {
                case TweenCommand.Play:
                    tweener.Play();
                    break;

                case TweenCommand.PlayForward:
                    tweener.PlayForward();
                    break;

                case TweenCommand.PlayBackward:
                    tweener.PlayBackward();
                    break;

                case TweenCommand.Pause:
                    tweener.Pause();
                    break;

                case TweenCommand.Unpause:
                    tweener.Unpause();
                    break;

                case TweenCommand.Complete:
                    tweener.Complete();
                    break;

                case TweenCommand.Rewind:
                    tweener.Rewind();
                    break;
            }
        }
    }
}

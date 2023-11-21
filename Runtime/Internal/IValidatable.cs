namespace Gilzoide.TweenJobs.Internal
{
    public interface IValidatable
    {
#if UNITY_EDITOR
        void OnValidate();
#endif
    }
}

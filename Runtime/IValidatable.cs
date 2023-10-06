namespace Gilzoide.TweenJobs
{
    public interface IValidatable
    {
#if UNITY_EDITOR
        void OnValidate();
#endif
    }
}

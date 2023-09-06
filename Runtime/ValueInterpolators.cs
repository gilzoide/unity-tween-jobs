using Unity.Mathematics;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public interface IInterpolator<T>
    {
        T Interpolate(T from, T to, float t);
    }

    public struct FloatInterpolator : IInterpolator<float>
    {
        public float Interpolate(float from, float to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector2Interpolator : IInterpolator<Vector2>
    {
        public Vector2 Interpolate(Vector2 from, Vector2 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector3Interpolator : IInterpolator<Vector3>
    {
        public Vector3 Interpolate(Vector3 from, Vector3 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector4Interpolator : IInterpolator<Vector4>
    {
        public Vector4 Interpolate(Vector4 from, Vector4 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct QuaternionInterpolator : IInterpolator<Quaternion>
    {
        public Quaternion Interpolate(Quaternion from, Quaternion to, float t)
        {
            return math.slerp(from, to, t);
        }
    }

    public struct ColorInterpolator : IInterpolator<Color>
    {
        public Color Interpolate(Color from, Color to, float t)
        {
            return math.lerp(from.ToFloat4(), to.ToFloat4(), t).ToColor();
        }
    }

    public struct Color32Interpolator : IInterpolator<Color32>
    {
        public Color32 Interpolate(Color32 from, Color32 to, float t)
        {
            return Color32.Lerp(from, to, t);
        }
    }

    public struct RectInterpolator : IInterpolator<Rect>
    {
        public Rect Interpolate(Rect from, Rect to, float t)
        {
            return math.lerp(from.ToFloat4(), to.ToFloat4(), t).ToRect();
        }
    }
}

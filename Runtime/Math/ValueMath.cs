using Unity.Mathematics;
using UnityEngine;

namespace Gilzoide.TweenJobs.Math
{
    public interface IValueMath<T>
    {
        T Add(T a, T b);
        T Interpolate(T from, T to, float t);
    }

    public struct FloatMath : IValueMath<float>
    {
        public float Add(float a, float b)
        {
            return a + b;
        }

        public float Interpolate(float from, float to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector2Math : IValueMath<Vector2>
    {
        public Vector2 Add(Vector2 a, Vector2 b)
        {
            return a + b;
        }

        public Vector2 Interpolate(Vector2 from, Vector2 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector3Math : IValueMath<Vector3>
    {
        public Vector3 Add(Vector3 a, Vector3 b)
        {
            return a + b;
        }

        public Vector3 Interpolate(Vector3 from, Vector3 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct Vector4Math : IValueMath<Vector4>
    {
        public Vector4 Add(Vector4 a, Vector4 b)
        {
            return a + b;
        }

        public Vector4 Interpolate(Vector4 from, Vector4 to, float t)
        {
            return math.lerp(from, to, t);
        }
    }

    public struct QuaternionMath : IValueMath<Quaternion>
    {
        public Quaternion Add(Quaternion a, Quaternion b)
        {
            return a * b;
        }

        public Quaternion Interpolate(Quaternion from, Quaternion to, float t)
        {
            return math.slerp(from, to, t);
        }
    }

    public struct ColorMath : IValueMath<Color>
    {
        public Color Add(Color a, Color b)
        {
            return a * b;
        }

        public Color Interpolate(Color from, Color to, float t)
        {
            return math.lerp(from.ToFloat4(), to.ToFloat4(), t).ToColor();
        }
    }

    public struct RectMath : IValueMath<Rect>
    {
        public Rect Add(Rect a, Rect b)
        {
            return new Rect(a.position + b.position, a.size + b.size);
        }

        public Rect Interpolate(Rect from, Rect to, float t)
        {
            return math.lerp(from.ToFloat4(), to.ToFloat4(), t).ToRect();
        }
    }
}

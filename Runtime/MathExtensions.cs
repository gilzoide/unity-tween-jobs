using Unity.Mathematics;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public static class MathExtensions
    {
        public static float4 ToFloat4(this Rect rect)
        {
            return math.float4(rect.x, rect.y, rect.width, rect.height);
        }

        public static Rect ToRect(this float4 v)
        {
            return new Rect(v.x, v.y, v.z, v.w);
        }

        public static float4 ToFloat4(this Color color)
        {
            return math.float4(color.r, color.g, color.b, color.a);
        }

        public static Color ToColor(this float4 v)
        {
            return new Color(v.x, v.y, v.z, v.w);
        }
    }
}

using System;
using UnityEngine;

namespace Gilzoide.TweenJobs
{
    public enum LoopType
    {
        Restart,
        PingPong,
    }

    public static class LoopTypeExtensions
    {
        public static float LoopValue(this LoopType loopType, float value, float length)
        {
            switch (loopType)
            {
                case LoopType.Restart:
                    return Mathf.Repeat(value, length);
                case LoopType.PingPong:
                    return Mathf.PingPong(value, length);
                default:
                    throw new ArgumentOutOfRangeException(nameof(loopType));
            }
        }
    }
}

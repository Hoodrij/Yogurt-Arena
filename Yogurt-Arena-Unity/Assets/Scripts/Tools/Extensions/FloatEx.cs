using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Yogurt.Arena
{
    public static class FloatEx
    {
        public static float Abs(this float f)
        {
            return Mathf.Abs(f);
        }
        
        public static float RandomTo(this float f)
        {
            return Random.Range(0, f);
        }

        public static TimeSpan ToSeconds(this float f)
        {
            return TimeSpan.FromSeconds(f);
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
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
        
        public static float DotToAngle(this float dot)
        {
            return Mathf.Acos(dot) *  Mathf.Rad2Deg;
        }
        
        public static float WithRandomSign(this float f)
        {
            return f * (true.RandomBool() ? 1 : -1);
        }

        public static void Clamp(ref this float f, float min, float max)
        {
            f = Mathf.Clamp(f, min, max);
        }
        
        public static TimeSpan Seconds(this float f)
        {
            return TimeSpan.FromSeconds(f);
        }
        
        public static UniTask Seconds(this float f, Lifetime life = null)
        {
            return Wait.Seconds(f, life);
        }
    }
}
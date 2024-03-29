﻿using System;
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
    }
}
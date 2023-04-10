using System;

namespace Yogurt.Arena
{
    public static class IntEx
    {
        public static TimeSpan ToSeconds(this int i)
        {
            return TimeSpan.FromSeconds(i);
        }
        
        public static int RandomTo(this int i)
        {
            return UnityEngine.Random.Range(0, i);
        }
    }
}
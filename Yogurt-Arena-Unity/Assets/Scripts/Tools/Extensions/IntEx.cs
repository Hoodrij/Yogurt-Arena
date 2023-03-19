using System;

namespace Yogurt.Arena
{
    public static class IntEx
    {
        public static TimeSpan Seconds(this int i)
        {
            return TimeSpan.FromSeconds(i);
        }
    }
}
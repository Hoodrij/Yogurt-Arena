﻿namespace Yogurt.Arena
{
    public class Level : IComponent
    {
        public int Current = 1;
        
        public static implicit operator int(Level level)
        {
            return level.Current;
        }
    }
}
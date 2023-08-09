using System;

namespace Yogurt.Arena
{
    [Flags]
    public enum AgentType
    {
        Player = 1 << 1,
        
        Charge = 1 << 2,
        Bomb = 1 << 3,
    }
}
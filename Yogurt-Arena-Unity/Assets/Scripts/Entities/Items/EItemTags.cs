using System;

namespace Yogurt.Arena
{
    [Flags]
    public enum EItemTags : byte
    {
        None = 0,
        Weapon = 1 << 0,
        AvailableToPlayer = 1 << 1,
        AvailableToEnemy = 1 << 2,
        
        Any = byte.MaxValue
    }
}
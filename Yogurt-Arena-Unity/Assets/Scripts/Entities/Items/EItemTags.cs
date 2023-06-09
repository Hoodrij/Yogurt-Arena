using System;

namespace Yogurt.Arena
{
    [Flags]
    public enum EItemTags : byte
    {
        None = 0,
        Weapon = 1 << 0,
        PlayerUsed = 1 << 1,
        EnemyUsed = 1 << 2,
        
        Any = byte.MaxValue
    }
}
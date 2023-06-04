using System;

namespace Yogurt.Arena
{
    [Flags]
    public enum EItemTags
    {
        None = 0,
        Weapon = 1 << 0,
        
    }
}
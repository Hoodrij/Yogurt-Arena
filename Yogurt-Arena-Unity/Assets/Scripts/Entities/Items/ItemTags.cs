namespace Yogurt.Arena
{
    [Flags]
    public enum ItemTags
    {
        None = 0,
        Weapon = 1 << 0,
        AvailableToPlayer = 1 << 1,
        AvailableToEnemy = 1 << 2,
        
        Any = int.MaxValue,
    }
}
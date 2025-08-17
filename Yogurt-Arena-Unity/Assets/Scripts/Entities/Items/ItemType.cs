namespace Yogurt.Arena
{
    [Flags]
    public enum ItemType
    {
        Empty = 0,
        
        Rifle = 1 << 1,
        Rain = 1 << 2,
        Charge = 1 << 3,
        SelfExplosion = 1 << 4,
        Shotgun = 1 << 5,
        
        TutorialRifle = 1 << 20,
        
        HealingPotion = 1 << 30,
        
        Any = int.MaxValue,
    }
}
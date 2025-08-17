namespace Yogurt.Arena;

[Flags]
public enum AgentType
{
    None = 0,
        
    Player = 1 << 1,
    Dummy = 1 << 2,
        
    Charge = 1 << 5,
    SelfExplosion = 1 << 6,
        
    Any = int.MaxValue,
}
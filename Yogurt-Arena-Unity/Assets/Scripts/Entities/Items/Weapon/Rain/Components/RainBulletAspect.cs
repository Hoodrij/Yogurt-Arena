namespace Yogurt.Arena;

public record struct RainBulletAspect(Entity Entity) : IAspect
{
    public RainBulletConfig Config => this.Get<RainBulletConfig>();
        
    public BulletAspect BulletAspect => this.As<BulletAspect>();
    public ref BattleState BattleState => ref this.Get<BattleState>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
}
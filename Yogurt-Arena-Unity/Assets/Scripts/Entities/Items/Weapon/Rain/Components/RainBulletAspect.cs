namespace Yogurt.Arena;

public struct RainBulletAspect : IAspect
{
    public Entity Entity { get; set; }

    public RainBulletConfig Config => this.Get<RainBulletConfig>();
        
    public BulletAspect BulletAspect => this.As<BulletAspect>();
    public ref BattleState BattleState => ref this.Get<BattleState>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
}
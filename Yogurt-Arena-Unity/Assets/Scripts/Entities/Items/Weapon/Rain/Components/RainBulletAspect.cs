namespace Yogurt.Arena;

public struct RainBulletAspect : IAspect
{
    public Entity Entity { get; set; }

    public RainBulletConfig Config => this.Get<RainBulletConfig>();
        
    public BulletAspect BulletAspect => this.As<BulletAspect>();
    public BattleState BattleState => this.Get<BattleState>();
    public OwnerState Owner => this.Get<OwnerState>();
}
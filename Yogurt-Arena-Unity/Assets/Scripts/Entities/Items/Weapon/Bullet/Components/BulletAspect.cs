namespace Yogurt.Arena;

public struct BulletAspect : IAspect
{
    public Entity Entity { get; set; }
        
    public BulletConfig Config => this.Get<BulletConfig>();

    public BodyState Body => this.Get<BodyState>();
    public BulletState State => this.Get<BulletState>();
    public OwnerState Owner => this.Get<OwnerState>();
    public BulletView View => this.Get<BulletView>();
}
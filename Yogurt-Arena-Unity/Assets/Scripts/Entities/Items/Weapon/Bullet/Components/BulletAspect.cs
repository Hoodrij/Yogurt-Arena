namespace Yogurt.Arena;

public struct BulletAspect : IAspect
{
    public Entity Entity { get; set; }
        
    public BulletConfig Config => this.Get<BulletConfig>();

    public ref BodyState Body => ref this.Get<BodyState>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
    public ref BulletView View => ref this.Get<BulletView>();
}
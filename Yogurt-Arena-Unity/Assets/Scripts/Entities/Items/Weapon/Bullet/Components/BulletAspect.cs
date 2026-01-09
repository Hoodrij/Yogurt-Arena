namespace Yogurt.Arena;

public record struct BulletAspect(Entity Entity) : IAspect
{
    public BulletConfig Config => this.Get<BulletConfig>();

    public ref BodyState Body => ref this.Get<BodyState>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
    public ref BulletView View => ref this.Get<BulletView>();
}
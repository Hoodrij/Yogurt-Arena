namespace Yogurt.Arena;

public record struct ItemSpotAspect(Entity Entity) : IAspect
{
    public ref ItemSpotConfig Config => ref this.Get<ItemSpotConfig>();

    public ref ItemSpotState State => ref this.Get<ItemSpotState>();
    public ref BodyState Body => ref this.Get<BodyState>();
    public ref ItemSpotView View => ref this.Get<ItemSpotView>();
}
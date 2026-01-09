namespace Yogurt.Arena;

public struct ItemSpotAspect : IAspect
{
    public Entity Entity { get; set; }
        
    public ref ItemSpotConfig Config => ref this.Get<ItemSpotConfig>();

    public ref ItemSpotState State => ref this.Get<ItemSpotState>();
    public ref BodyState Body => ref this.Get<BodyState>();
    public ref ItemSpotView View => ref this.Get<ItemSpotView>();
}
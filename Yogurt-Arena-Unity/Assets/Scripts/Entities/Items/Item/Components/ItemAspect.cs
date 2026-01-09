namespace Yogurt.Arena;

public record struct ItemAspect(Entity Entity) : IAspect 
{
    public ItemConfig Config => this.Get<ItemConfig>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
}
    
public record struct ItemConfigAspect(Entity Entity) : IAspect 
{
    public ItemConfig Item => this.Get<ItemConfig>();
    public ref EntityBlueprint Blueprint => ref this.Get<EntityBlueprint>();
}
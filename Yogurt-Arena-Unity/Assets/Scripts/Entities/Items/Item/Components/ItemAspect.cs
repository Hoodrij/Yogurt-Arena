namespace Yogurt.Arena;

public struct ItemAspect : IAspect 
{
    public Entity Entity { get; set; }
        
    public ItemConfig Config => this.Get<ItemConfig>();
    public ref OwnerState Owner => ref this.Get<OwnerState>();
}
    
public struct ItemConfigAspect : IAspect 
{
    public Entity Entity { get; set; }
        
    public ItemConfig Item => this.Get<ItemConfig>();
    public ref EntityBlueprint Blueprint => ref this.Get<EntityBlueprint>();
}
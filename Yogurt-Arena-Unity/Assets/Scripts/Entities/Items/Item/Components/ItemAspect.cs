namespace Yogurt.Arena
{
    public struct ItemAspect : IAspect 
    {
        public Entity Entity { get; set; }
        
        public ItemConfig Config => this.Get<ItemConfig>();
        public OwnerState Owner => this.Get<OwnerState>();
    }
}
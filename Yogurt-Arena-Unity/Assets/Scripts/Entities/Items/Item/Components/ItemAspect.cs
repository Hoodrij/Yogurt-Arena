namespace Yogurt.Arena
{
    public struct ItemAspect : IAspect 
    {
        public Entity Entity { get; set; }
        
        public ItemConfig Config => this.Get<ItemConfig>();
        public OwnerState Owner => this.Get<OwnerState>();
    }
    
    public struct ItemConfigAspect : IAspect 
    {
        public Entity Entity { get; set; }
        
        public ItemConfig Item => this.Get<ItemConfig>();
        public ConfigEntity ConfigEntity => this.Get<ConfigEntity>();
    }
}
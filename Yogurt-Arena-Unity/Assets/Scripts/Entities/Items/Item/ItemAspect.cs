namespace Yogurt.Arena
{
    public struct ItemAspect : IAspect 
    {
        public Entity Entity { get; set; }
        
        public Item Item => this.Get<Item>();
        public OwnerState Owner => this.Get<OwnerState>();
    }
}
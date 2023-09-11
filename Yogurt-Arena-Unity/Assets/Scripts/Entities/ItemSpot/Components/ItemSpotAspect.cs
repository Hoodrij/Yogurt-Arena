namespace Yogurt.Arena
{
    public struct ItemSpotAspect : IAspect
    {
        public Entity Entity { get; set; }
        
        public ItemSpotConfig Config => this.Get<ItemSpotConfig>();

        public ItemSpotState State => this.Get<ItemSpotState>();
        public BodyState Body => this.Get<BodyState>();
        public ItemSpotView View => this.Get<ItemSpotView>();
    }
}
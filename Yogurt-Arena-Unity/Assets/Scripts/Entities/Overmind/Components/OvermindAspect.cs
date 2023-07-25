namespace Yogurt.Arena
{
    public struct OvermindAspect : IAspect
    {
        public Entity Entity { get; set; }

        public OvermindConfig Config => this.Get<OvermindConfig>();

        public OvermindState State => this.Get<OvermindState>();
    }
}
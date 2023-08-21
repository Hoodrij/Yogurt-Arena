namespace Yogurt.Arena
{
    public struct OvermindAspect : IAspect
    {
        public Entity Entity { get; set; }

        public OvermindConfig Config => new GetConfigJob().Run<OvermindConfig>();

        public OvermindState State => this.Get<OvermindState>();
    }
}
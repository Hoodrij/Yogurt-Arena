namespace Yogurt.Arena
{
    public struct OvermindAspect : IAspect
    {
        public Entity Entity { get; set; }

        public OvermindData Data => this.Get<OvermindData>();

        public OvermindState State => this.Get<OvermindState>();
    }
}
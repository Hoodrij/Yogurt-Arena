namespace Yogurt.Arena
{
    public struct OvermindAspect : IAspect
    {
        public Entity Entity { get; set; }

        public OvermindState State => this.Get<OvermindState>();
    }
}
namespace Yogurt.Arena
{
    public struct InputAspect : IAspect
    {
        public Entity Entity { get; set; }

        public InputState State => this.Get<InputState>();
        public InputConfig Config => this.Get<InputConfig>();
    }
}
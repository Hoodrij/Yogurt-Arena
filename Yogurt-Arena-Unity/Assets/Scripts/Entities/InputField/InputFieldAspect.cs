namespace Yogurt.Arena
{
    public struct InputFieldAspect : IAspect
    {
        public Entity Entity { get; set; }

        public InputState Input => this.Get<InputState>();
        public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
    }
}
namespace Yogurt.Arena
{
    public struct InputFieldAspect : IAspect
    {
        public Entity Entity { get; set; }

        public InputValues Input => this.Get<InputValues>();
        public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
    }
}
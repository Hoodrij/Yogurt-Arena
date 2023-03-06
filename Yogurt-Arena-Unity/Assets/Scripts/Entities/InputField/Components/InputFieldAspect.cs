namespace Yogurt.Arena
{
    public struct InputFieldAspect : IAspect
    {
        public Entity Entity { get; set; }

        public InputData Input => this.Get<InputData>();
        public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
    }
}
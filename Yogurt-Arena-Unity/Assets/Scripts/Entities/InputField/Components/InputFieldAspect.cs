namespace Yogurt.Arena
{
    public struct InputFieldAspect : IAspect
    {
        public Entity Entity { get; set; }

        public InputData Data => this.Get<InputData>();
        
        public InputState Input => this.Get<InputState>();
        public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
    }
}
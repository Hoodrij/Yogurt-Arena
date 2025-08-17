namespace Yogurt.Arena;

public struct InputFieldAspect : IAspect
{
    public Entity Entity { get; set; }

    public InputConfig Config => this.Get<InputConfig>();
        
    public InputState Input => this.Get<InputState>();
    public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
}
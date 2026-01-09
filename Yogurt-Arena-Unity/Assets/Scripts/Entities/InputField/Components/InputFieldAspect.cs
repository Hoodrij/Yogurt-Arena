namespace Yogurt.Arena;

public record struct InputFieldAspect(Entity Entity) : IAspect
{
    public InputConfig Config => this.Get<InputConfig>();
        
    public ref InputState Input => ref this.Get<InputState>();
    public MoveInputReader MoveInputReader => this.Get<MoveInputReader>();
}
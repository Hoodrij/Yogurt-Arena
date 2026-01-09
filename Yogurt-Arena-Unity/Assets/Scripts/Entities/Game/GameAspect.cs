namespace Yogurt.Arena;

public record struct GameAspect(Entity Entity) : IAspect
{
    public ref Game Game => ref this.Get<Game>();
    public ref Time Time => ref this.Get<Time>();
}
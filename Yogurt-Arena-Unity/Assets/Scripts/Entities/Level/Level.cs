namespace Yogurt.Arena;

public record struct Level() : IComponent
{
    public int Current = 0;
}

public record struct LevelAspect(Entity Entity) : IAspect
{
    public ref Level Level => ref this.Get<Level>();
}
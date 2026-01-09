namespace Yogurt.Arena;

public record struct LocationAspect(Entity Entity) : IAspect
{
    public ref Location Location => ref this.Get<Location>();

    public NavMeshSurface NavSurface => Location.NavSurface;
}
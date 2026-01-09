namespace Yogurt.Arena;

public struct LocationAspect : IAspect
{
    public Entity Entity { get; set; }

    public ref Location Location => ref this.Get<Location>();

    public NavMeshSurface NavSurface => Location.NavSurface;
}
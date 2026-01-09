namespace Yogurt.Arena;

public record struct OvermindAspect(Entity Entity) : IAspect
{
    public OvermindConfig Config => new GetConfigJob().Run<OvermindConfig>();

    public ref OvermindState State => ref this.Get<OvermindState>();
}
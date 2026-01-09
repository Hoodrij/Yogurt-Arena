namespace Yogurt.Arena;

public struct OvermindAspect : IAspect
{
    public Entity Entity { get; set; }

    public OvermindConfig Config => new GetConfigJob().Run<OvermindConfig>();

    public ref OvermindState State => ref this.Get<OvermindState>();
}
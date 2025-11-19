namespace Yogurt.Arena;

public struct PlayerAspect : IAspect
{
    public Entity Entity { get; set; }

    public PlayerTag Tag => this.Get<PlayerTag>();
    public AgentAspect Agent => this.As<AgentAspect>();
}
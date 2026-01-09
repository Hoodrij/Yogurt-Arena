namespace Yogurt.Arena;

public record struct PlayerAspect(Entity Entity) : IAspect
{
    public PlayerTag Tag => this.Get<PlayerTag>();
    public AgentAspect Agent => this.As<AgentAspect>();
}
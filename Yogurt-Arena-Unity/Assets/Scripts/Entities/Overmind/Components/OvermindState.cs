namespace Yogurt.Arena;

public record struct OvermindState() : IComponent
{
    public int TotalSpawned;
    public List<AgentAspect> CurrentAgents = new List<AgentAspect>();
}
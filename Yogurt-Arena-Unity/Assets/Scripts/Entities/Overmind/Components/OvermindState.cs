namespace Yogurt.Arena;

public class OvermindState : IComponent
{
    public int TotalSpawned;
    public List<AgentAspect> CurrentAgents = new List<AgentAspect>();

    public async UniTaskVoid KeepAgent(AgentAspect agent)
    {
        TotalSpawned += 1;
        CurrentAgents.Add(agent);
        await agent.Life();
        CurrentAgents.Remove(agent);
    } 
}
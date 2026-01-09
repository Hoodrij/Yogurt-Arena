namespace Yogurt.Arena;

public struct SpawnSingleEnemyJob
{
    public async Task<AgentAspect> Run(OvermindAspect overmind)
    {
        Vector3 spawnPoint = await new GetFreeSpawnPointJob().Run(overmind);
        AgentConfig config = new GetAgentConfigJob().Run(TeamType.Red, overmind.Config.AvailableTypes);
                
        AgentAspect agent = await new AgentSpawnJob().Run(config, spawnPoint);
        new SpawnWorldHealthWidget().Run(agent).Forget();

        KeepAgent(overmind, agent).Forget();

        return agent;
        
        static async UniTaskVoid KeepAgent(OvermindAspect overmind, AgentAspect agent)
        {
            overmind.State.TotalSpawned += 1;
            overmind.State.CurrentAgents.Add(agent);
            await agent.Life();
            overmind.State.CurrentAgents.Remove(agent);
        } 
    }
}
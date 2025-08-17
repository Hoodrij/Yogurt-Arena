namespace Yogurt.Arena;

public struct SpawnSingleEnemyJob
{
    public async Task<AgentAspect> Run(OvermindAspect overmind)
    {
        Vector3 spawnPoint = await new GetFreeSpawnPointJob().Run(overmind);
        AgentConfig config = new GetAgentConfigJob().Run(TeamType.Red, overmind.Config.AvailableTypes);
                
        AgentAspect agent = await new AgentSpawnJob().Run(config, spawnPoint);
        new SpawnWorldHealthWidget().Run(agent).Forget();

        overmind.State.KeepAgent(agent).Forget();

        return agent;
    }
}
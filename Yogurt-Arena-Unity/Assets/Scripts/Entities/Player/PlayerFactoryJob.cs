namespace Yogurt.Arena;

public struct PlayerFactoryJob
{
    public async UniTask<PlayerAspect> Run()
    {
        AgentConfig config = new GetAgentConfigJob().Run(TeamType.Green);

        AgentAspect agent = await new AgentSpawnJob().Run(config, Vector3.zero);
        agent.Add(new PlayerTag());
        agent.Health.HealthWidget = Query.Single<UIView>().HealthWidget;

        new UpdatePlayerDestinationJob().Run(agent.As<PlayerAspect>());

        return agent.As<PlayerAspect>();
    }
}
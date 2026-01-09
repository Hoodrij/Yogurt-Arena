namespace Yogurt.Arena;

public struct AgentFactoryJob
{
    public async UniTask<AgentAspect> Run(AgentConfig config, TeamType teamType)
    {
        AgentView agentView = await config.Asset.Spawn();
            
        AgentAspect agent = World.Create()
            .Link(agentView.gameObject)
            .Add(config)
            .Add(agentView)
            .Add(new BodyState())
            .Add(new BattleState())
            .Add(new Inventory())
            .Add(new AgentId
            {
                TeamType = teamType
            })
            .Add(new Health
            {
                MaxHealth = config.MaxHealth,
                Value = config.Health,
                DeathJob = new AgentDeathJob(),
            })
            .As<AgentAspect>();
            
        return agent;
    }
}
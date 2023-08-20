using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run(AgentConfig config, TeamType teamType)
        {
            AgentView agentView = await config.Asset.Spawn();
            
            AgentAspect agent = World.Create()
                .AddLink(agentView.gameObject)
                .Add(config)
                .Add(agentView)
                .Add(new BodyState())
                .Add(new BattleState())
                .Add(new Inventory())
                .Add(new AgentId
                {
                    teamType = teamType
                })
                .Add(new Health
                {
                    MaxHealth = config.MaxHealth,
                    Value = config.Health
                })
                .As<AgentAspect>();

            new AgentMoveJob().Run(agent);
            new AgentLookJob().Run(agent);

            await new GiveItemJob().Run(agent.Config.Weapon, agent);
            
            return agent;
        }
    }
}
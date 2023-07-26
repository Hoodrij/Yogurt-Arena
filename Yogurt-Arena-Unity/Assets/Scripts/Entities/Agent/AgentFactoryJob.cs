using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async Awaitable<AgentAspect> Run(AgentConfig config, Team team)
        {
            AgentView agentView = await config.Asset.Spawn();
            
            AgentAspect agent = World.Create()
                .AddLink(agentView.gameObject)
                .Add(config)
                .Add(agentView)
                .Add<BodyState>()
                .Add<BattleState>()
                .Add<Inventory>()
                .Add(new AgentId
                {
                    Team = team
                })
                .Add(new Health
                {
                    MaxHealth = config.MaxHealth,
                    Value = config.Health
                })
                .As<AgentAspect>();

            new AgentMoveJob().Run(agent);
            new AgentLookJob().Run(agent);
            
            return agent;
        }
    }
}
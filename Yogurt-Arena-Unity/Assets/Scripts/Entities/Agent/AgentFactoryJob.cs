using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async Awaitable<AgentAspect> Run(AgentConfig config, Team team)
        {
            AgentView agentView = await config.Asset.Spawn();
            
            Entity entity = World.Create()
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
                });
            
            return entity.As<AgentAspect>();
        }
    }
}
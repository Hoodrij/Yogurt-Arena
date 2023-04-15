using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run(AgentData data, Team team)
        {
            AgentView agentView = await data.Asset.Spawn();
            
            Entity entity = World.Create()
                .AddLink(agentView.gameObject)
                .Add(data)
                .Add(agentView)
                .Add<BodyState>()
                .Add<AgentBattleState>()
                .Add<ItemsCollection>()
                .Add(new AgentId
                {
                    Team = team
                })
                .Add(new Health
                {
                    Value = data.Health
                });
            
            return entity.As<AgentAspect>();
        }
    }
}
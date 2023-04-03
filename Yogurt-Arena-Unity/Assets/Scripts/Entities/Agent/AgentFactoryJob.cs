using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run(IAsset<AgentView> asset, Team team)
        {
            AgentView agentView = await asset.Spawn();
            
            Entity entity = World.Create()
                .AddLink(agentView.gameObject)
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
                    Value = 5
                });
            
            return entity.As<AgentAspect>();
        }
    }
}
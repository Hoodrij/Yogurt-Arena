using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run(Asset<AgentView> asset, Team team)
        {
            AgentView agentView = await asset.Spawn();
            
            Entity entity = World.Create()
                .AddDisposable(agentView)
                .Add<BodyState>()
                .Add<AgentBattleState>()
                .Add<ItemsCollection>()
                .Add(new AgentId
                {
                    Team = team
                });

            return entity.As<AgentAspect>();
        }
    }
}
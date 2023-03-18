using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run(Asset<AgentView> asset)
        {
            AgentView agentView = await asset.Spawn();
            
            Entity entity = World.Create()
                .AddDisposable(agentView)
                .Add<AgentState>();

            return entity.As<AgentAspect>();
        }
    }
}
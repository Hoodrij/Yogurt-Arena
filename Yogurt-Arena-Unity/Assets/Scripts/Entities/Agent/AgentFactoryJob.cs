using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<AgentAspect> Run()
        {
            AgentView agentView = await Query.Single<Assets>().Agent.Spawn();
            
            Entity entity = World.Create()
                .AddDisposable(agentView)
                .Add<AgentState>();

            return entity.As<AgentAspect>();
        }
    }
}
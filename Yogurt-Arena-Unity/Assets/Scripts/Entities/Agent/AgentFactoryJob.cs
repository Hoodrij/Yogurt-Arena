using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct AgentFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            AgentView agentView = await Query.Single<Assets>().Agent.Spawn();
            
            Entity entity = World.Create()
                .AddDisposable(agentView)
                .Add<AgentState>();

            return entity;
        }
    }
}
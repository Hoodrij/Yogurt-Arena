using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            AgentAspect agent = await new AgentFactoryJob().Run();
            agent.Add<PlayerTag>();

            return agent.Entity;
        }
    }
}
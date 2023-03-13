using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Entity entity = await new AgentFactoryJob().Run();
            entity.Add<PlayerTag>();

            return entity;
        }
    }
}
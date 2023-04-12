using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct LevelFactoryJob
    {
        public async UniTask Run()
        {
            Level view = await Query.Single<Data>().Level.Spawn();

            Entity entity = World.Create()
                .Add(view);
        }
    }
}
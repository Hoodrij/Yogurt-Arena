using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct LevelFactoryJob
    {
        public async UniTask Run()
        {
            Entity entity = World.Create()
                .Add(await Query.Single<Assets>().Level.Spawn());
        }
    }
}
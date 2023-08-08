using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerFactory
    {
        public async UniTask Run()
        {
            Entity entity = World.Create()
                .Add<ItemsSpawnerState>();

            new ItemsSpawnerBehaviorJob().Run(entity);
        }
    }
}
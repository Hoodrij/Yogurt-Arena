using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerFactory
    {
        public async UniTask Run()
        {
            ItemSpawnerAspect itemSpawner = World.Create()
                .Add<ItemSpawnerConfig>()
                .Add<ItemsSpawnerState>()
                .As<ItemSpawnerAspect>();

            new ItemsSpawnerBehaviorJob().Run(itemSpawner);
        }
    }
}
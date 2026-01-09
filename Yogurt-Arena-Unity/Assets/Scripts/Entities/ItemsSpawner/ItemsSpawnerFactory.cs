namespace Yogurt.Arena;

public struct ItemsSpawnerFactory
{
    public async UniTask Run()
    {
        ItemSpawnerAspect itemSpawner = World.Create()
            .As<ItemSpawnerAspect>();

        new ItemsSpawnerBehaviorJob().Run(itemSpawner).Forget();
    }
}
namespace Yogurt.Arena;

public struct ItemsSpawnerFactory
{
    public async UniTask Run()
    {
        ItemSpawnerAspect itemSpawner = World.Create()
            .Add(new ItemsSpawnerState())
            .As<ItemSpawnerAspect>();

        new ItemsSpawnerBehaviorJob().Run(itemSpawner).Forget();
    }
}
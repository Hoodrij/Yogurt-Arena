namespace Yogurt.Arena;

public struct ItemSpawnerAspect : IAspect
{
    public Entity Entity { get; set; }

    public ItemSpawnerConfig Config => new GetConfigJob().Run<ItemSpawnerConfig>();
}
namespace Yogurt.Arena;

public record struct ItemSpawnerAspect(Entity Entity) : IAspect
{
    public ItemSpawnerConfig Config => new GetConfigJob().Run<ItemSpawnerConfig>();
}
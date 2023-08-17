namespace Yogurt.Arena
{
    public struct ItemSpawnerAspect : IAspect
    {
        public Entity Entity { get; set; }

        public ItemsSpawnerState State => this.Get<ItemsSpawnerState>();

        public ItemSpawnerConfig Config => new GetConfigJob().Run<ItemSpawnerConfig>();
    }
}
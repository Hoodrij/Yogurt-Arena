namespace Yogurt.Arena;

public struct SpawnItemDropJob
{
    public async UniTask Run(Vector3 position)
    {
        ItemSpawnerConfig itemSpawnerConfig = new GetConfigJob().Run<ItemSpawnerConfig>();
        ItemSpotConfig itemSpotConfig = new GetConfigJob().Run<ItemSpotConfig>();

        ItemSpotView view = await itemSpotConfig.DropAsset.Spawn();
        view.transform.position = position;

        ItemSpotAspect itemSpot = await new ItemSpotFactoryJob().Run(view, true);
        itemSpot.State.Type = new GetRandomItemJob().Run(
            itemSpawnerConfig.AvailableTags,
            itemSpawnerConfig.AvailableItems);
    }
}

namespace Yogurt.Arena;

public struct ItemSpotFactoryJob
{
    public async UniTask<ItemSpotAspect> Run(ItemSpotView view)
    {
        ItemSpotConfig config = new GetConfigJob().Run<ItemSpotConfig>();
            
        ItemSpotAspect entity = World.Create()
            .Link(view.gameObject)
            .Add(view)
            .Add(config)
            .Add(new BodyState
            {
                Position = view.transform.position
            })
            .Add(new ItemSpotState
            {
                Type = ItemType.Empty,
            })
            .As<ItemSpotAspect>();
            
        new ItemSpotBehaviorJob().Run(entity).Forget();

        return entity;
    }
}
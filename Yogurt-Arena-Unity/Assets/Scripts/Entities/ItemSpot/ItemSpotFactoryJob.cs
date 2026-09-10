namespace Yogurt.Arena;

public struct ItemSpotFactoryJob
{
    public async UniTask<ItemSpotAspect> Run(ItemSpotView view, bool isTransient = false)
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

        if (isTransient)
        {
            entity.Add(new ItemDropTag());
        }
            
        new ItemSpotBehaviorJob().Run(entity).Forget();

        return entity;
    }
}
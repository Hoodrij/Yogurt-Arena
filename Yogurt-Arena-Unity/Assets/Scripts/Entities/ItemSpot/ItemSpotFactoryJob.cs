using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotFactoryJob
    {
        public async UniTask<ItemSpotAspect> Run(ItemSpotView view)
        {
            ItemSpotConfig config = Query.Single<Config>().ItemSpot;
            
            Entity entity = World.Create();
            entity
                .AddLink(view.gameObject)
                .Add(view)
                .Add(new BodyState
                {
                    Position = view.transform.position
                })
                .Add(new ItemSpotState
                {
                    Type = EItemType.Empty,
                    Radius = config.Radius,
                    Mask = config.Mask
                });

            return entity.As<ItemSpotAspect>();
        }
    }
}
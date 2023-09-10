using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotFactoryJob
    {
        public async UniTask<ItemSpotAspect> Run(ItemSpotView view)
        {
            ItemSpotConfig config = new GetConfigJob().Run<ItemSpotConfig>();
            
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
                    Type = ItemType.Empty,
                    Radius = config.Radius,
                    Mask = config.Mask
                });

            return entity.As<ItemSpotAspect>();
        }
    }
}
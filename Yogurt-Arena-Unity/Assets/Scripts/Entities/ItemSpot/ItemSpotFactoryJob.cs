using UnityEngine;

namespace Yogurt.Arena
{
    public struct ItemSpotFactoryJob
    {
        public async Awaitable<ItemSpotAspect> Run(ItemSpotView view)
        {
            ItemSpotData data = Query.Single<Data>().ItemSpot;
            
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
                    Radius = data.Radius,
                    Mask = data.Mask
                });

            return entity.As<ItemSpotAspect>();
        }
    }
}
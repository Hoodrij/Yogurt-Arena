using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotFactoryJob
    {
        public async UniTask<ItemSpotAspect> Run(ItemSpotAuthoring authoring)
        {
            Entity entity = World.Create();
            entity.Add(new BodyState
                {
                    Position = authoring.transform.position
                })
                .Add(new ItemSpotState
                {
                    Type = authoring.ItemType,
                    Radius = authoring.Radius,
                    Mask = authoring.Mask
                });

            return entity.As<ItemSpotAspect>();
        }
    }
}
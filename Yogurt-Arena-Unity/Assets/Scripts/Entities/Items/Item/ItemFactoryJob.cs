using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IItemFactoryJob
    {
        UniTask Run(ItemAspect item);
    }
    
    public struct ItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(ItemType type, AgentAspect owner)
        {
            ItemConfigAspect config = new GetConfigJob().Run(type);
            EntityConfig entityConfig = config.EntityConfig;

            Entity entity = World.Create()
                .Add(entityConfig.Components)
                .Add(new OwnerState
                {
                    Owner = owner
                });
            
            entity.SetParent(owner.Entity);
            
            ItemAspect item = entity.As<ItemAspect>();
            config.Config.FactoryJob.Run(item);

            return item;
        }
    }
}
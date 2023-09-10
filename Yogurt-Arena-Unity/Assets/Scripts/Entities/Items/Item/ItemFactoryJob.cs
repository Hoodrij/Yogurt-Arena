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
            ConfigOfEntity configOfEntity = config.ConfigOfEntity;

            Entity entity = World.Create()
                .Add(configOfEntity.Components)
                .Add(new OwnerState
                {
                    Value = owner
                });
            
            entity.SetParent(owner.Entity);
            
            ItemAspect item = entity.As<ItemAspect>();
            config.Item.FactoryJob.Run(item);

            return item;
        }
    }
}
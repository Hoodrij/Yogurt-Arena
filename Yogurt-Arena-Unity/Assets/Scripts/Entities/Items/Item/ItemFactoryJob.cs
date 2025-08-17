namespace Yogurt.Arena;

public interface IItemFactoryJob
{
    UniTask Run(ItemAspect item);
}
    
public struct ItemFactoryJob
{
    public async UniTask<ItemAspect> Run(ItemType type, AgentAspect owner)
    {
        ItemConfigAspect config = new GetConfigJob().Run(type);
        ConfigEntity configEntity = config.ConfigEntity;

        Entity entity = World.Create()
            .Add(configEntity.Components)
            .Add(new OwnerState
            {
                Value = owner
            });
            
        entity.SetParent(owner.Entity);
            
        ItemAspect item = entity.As<ItemAspect>();
        config.Item.FactoryJob?.Run(item);

        return item;
    }
}
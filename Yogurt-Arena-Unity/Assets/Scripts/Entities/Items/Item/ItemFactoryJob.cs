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
        EntityBlueprint blueprint = config.Blueprint;

        ItemAspect item = World.Create()
            .Add(new OwnerState
            {
                Value = owner
            })
            .PopulateFrom(blueprint)
            .SetParent(owner)
            .As<ItemAspect>();
        
        config.Item.FactoryJob?.Run(item);

        return item;
    }
}
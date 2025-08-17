namespace Yogurt.Arena;

public struct GetRandomItemJob
{
    public ItemType Run(ItemTags availableTags, ItemType availableTypes = ItemType.Any)
    {
        return Query.Of<ItemConfigAspect>().AsEnumerable()
            .Where(FitsTags)
            .Where(FitsType)
            .GetRandom()
            .Item.Type;


        bool FitsTags(ItemConfigAspect config)
        {
            return availableTags.HasFlag(config.Item.Tags);
        }
        bool FitsType(ItemConfigAspect config)
        {
            return availableTypes.HasFlag(config.Item.Type);
        }
    }
        
}
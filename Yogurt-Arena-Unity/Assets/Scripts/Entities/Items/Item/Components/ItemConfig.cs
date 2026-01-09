namespace Yogurt.Arena;

[Serializable]
public record struct ItemConfig : IComponent
{
    public ItemType Type;
    public ItemTags Tags;
    public IItemFactoryJob FactoryJob;
    public IItemUseJob UseJob;
}
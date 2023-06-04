namespace Yogurt.Arena
{
    public class Item : IComponent
    {
        public EItemType Type;
        public IItemUseJob Job;
    }
    
    public static class ItemEx
    {
        public static IItemFactoryJob GetFactoryJob(this EItemType type) => type switch
        {
            EItemType.Rifle => new RifleFactoryJob(),
            EItemType.Charge => new ChargeFactoryJob(),
            EItemType.Rain => new RainFactoryJob(),
        };

        public static EItemTags GetTags(this EItemType type) => type switch
        {
            EItemType.Rifle => EItemTags.Weapon,
            EItemType.Charge => EItemTags.Weapon,
            EItemType.Rain => EItemTags.Weapon
        };
        
        public static bool HasTags(this Item item, EItemTags others)
        {
            EItemTags tags = item.Type.GetTags();
            return tags.HasFlag(others);
        }
    }
}
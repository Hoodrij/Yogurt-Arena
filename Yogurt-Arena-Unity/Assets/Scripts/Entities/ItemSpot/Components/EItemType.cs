namespace Yogurt.Arena
{
    public enum EItemType
    {
        Rifle = 100,
        Rain = 101,
        Charge = 102,
    }

    public static class ItemTypeEx
    {
        public static IItemFactoryJob GetFactoryJob(this EItemType type) => type switch
        {
            EItemType.Rifle => new RifleFactoryJob(),
            EItemType.Charge => new ChargeFactoryJob(),
            EItemType.Rain => new RainFactoryJob(),
        };
    }
}
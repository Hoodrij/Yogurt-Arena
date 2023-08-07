using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class ItemConfig : IComponent
    {
        public EItemType Type;
        public EItemTags Tags;
        public IItemFactoryJob FactoryJob;
        public IItemUseJob UseJob;
    }
    
    public static class ItemEx
    {
        public static IItemFactoryJob GetFactoryJob(this EItemType type) => type switch
        {
            EItemType.Rifle => new RifleFactoryJob(),
            EItemType.Charge => new ChargeFactoryJob(),
            EItemType.Rain => new RainFactoryJob(),
            EItemType.HealingPotion => new HealingPotionFactoryJob()
        };

        public static EItemTags GetTags(this EItemType type) => type switch
        {
            EItemType.Rifle => EItemTags.Weapon | EItemTags.AvailableToPlayer,
            EItemType.Rain => EItemTags.Weapon | EItemTags.AvailableToPlayer,
            
            EItemType.Charge => EItemTags.Weapon | EItemTags.AvailableToEnemy,
            EItemType.SelfExplosion => EItemTags.Weapon | EItemTags.AvailableToEnemy,
            
            EItemType.HealingPotion => EItemTags.AvailableToPlayer,
            _ => EItemTags.None
        };
    }
}
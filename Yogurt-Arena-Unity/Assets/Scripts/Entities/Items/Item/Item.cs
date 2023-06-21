using System;
using System.Linq;

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
            EItemType.HealingPotion => new HealingPotionFactoryJob()
        };

        public static EItemTags GetTags(this EItemType type) => type switch
        {
            EItemType.Rifle => EItemTags.Weapon | EItemTags.PlayerUsed,
            EItemType.Rain => EItemTags.Weapon | EItemTags.PlayerUsed,
            EItemType.Charge => EItemTags.Weapon | EItemTags.EnemyUsed,
            EItemType.HealingPotion => EItemTags.PlayerUsed | EItemTags.Weapon,
            _ => EItemTags.None
        };

        public static EItemType GetRandom(this EItemType _, EItemTags tags = EItemTags.Any)
        {
            return Enum.GetValues(typeof(EItemType))
                .Cast<EItemType>()
                .Where(type => type.HasTags(tags))
                .GetRandom();
        }
        
        public static bool HasTags(this EItemType type, EItemTags others)
        {
            EItemTags tags = type.GetTags();
            return tags.HasFlag(others);
        }
    }
}
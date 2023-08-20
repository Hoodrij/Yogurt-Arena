using System.Linq;

namespace Yogurt.Arena
{
    public struct GetConfigJob
    {
        // of a Leveled Config
        public TConfig Run<TConfig>() where TConfig : IEntityConfig, IComponent
        {
            Level currentLevel = Query.Single<Level>();

            TConfig config = Query.Single<TConfig>();
            if (config is not ILeveledConfig)
            {
                return config;
            }

            foreach (Entity entity in Query.Of<TConfig>())
            {
                config = entity.Get<TConfig>();
                if (config is ILeveledConfig leveledConfig)
                {
                    if (leveledConfig.Level == currentLevel)
                    {
                        return config;
                    }
                }
            }
            
            //same
            // config = (TConfig) Query.Of<TConfig>()
                // .Select(entity => entity.Get<TConfig>() as ILeveledConfig)
                // .FirstOrDefault(leveledConfig => leveledConfig.Level == currentLevel);

            return config;
        }

        // of an Item
        public ItemConfigAspect Run(ItemType itemType)
        {
            ItemConfigAspect itemConfigAspect = Query.Of<ItemConfigAspect>()
                .FirstOrDefault(aspect => aspect.Item.Type == itemType);
            return itemConfigAspect;
        }
    }
}
namespace Yogurt.Arena
{
    public struct GetConfigJob
    {
        public TConfig Run<TConfig>() where TConfig : IConfig, IComponent
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
    }
}
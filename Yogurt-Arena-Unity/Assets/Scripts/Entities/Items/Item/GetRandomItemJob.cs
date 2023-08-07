using System.Linq;

namespace Yogurt.Arena
{
    public struct GetRandomItemJob
    {
        public EItemType Run(EItemTags requiredTags)
        {
            return Query.Of<ItemConfigAspect>()
                .Where(config => config.Config.Tags.HasFlag(requiredTags))
                .GetRandom()
                .Config.Type;
        }
        
    }
}
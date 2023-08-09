using System.Linq;

namespace Yogurt.Arena
{
    public struct GetRandomItemJob
    {
        public ItemType Run(ItemTags requiredTags)
        {
            return Query.Of<ItemConfigAspect>()
                .Where(config => config.Config.Tags.HasFlag(requiredTags))
                .GetRandom()
                .Config.Type;
        }
        
    }
}
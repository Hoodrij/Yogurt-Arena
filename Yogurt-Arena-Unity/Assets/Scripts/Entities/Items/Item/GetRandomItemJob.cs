using System.Linq;

namespace Yogurt.Arena
{
    public struct GetRandomItemJob
    {
        public ItemType Run(ItemTags requiredTags, ItemType specificType = ItemType.Any)
        {
            return Query.Of<ItemConfigAspect>()
                .Where(FitsTags)
                .Where(FitsType)
                .GetRandom()
                .Config.Type;


            bool FitsTags(ItemConfigAspect config)
            {
                return config.Config.Tags.HasFlag(requiredTags);
            }
            bool FitsType(ItemConfigAspect config)
            {
                if (specificType == ItemType.Any)
                {
                    return true;
                }

                return config.Config.Type == specificType;
            }
        }
        
    }
}
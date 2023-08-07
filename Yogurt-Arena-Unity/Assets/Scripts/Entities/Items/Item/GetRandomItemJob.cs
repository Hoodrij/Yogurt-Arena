using System;
using System.Linq;

namespace Yogurt.Arena
{
    public struct GetRandomItemJob
    {
        public EItemType Run(EItemTags requiredTags)
        {
            return Enum.GetValues(typeof(EItemType))
                .Cast<EItemType>()
                .Where(HasTags)
                .GetRandom();
            
            
            bool HasTags(EItemType type)
            {
                EItemTags tags = type.GetTags();
                return tags.HasFlag(requiredTags);
            }
        }
        
    }
}
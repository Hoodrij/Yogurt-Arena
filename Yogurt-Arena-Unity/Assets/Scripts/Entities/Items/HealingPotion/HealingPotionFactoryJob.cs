using UnityEngine;

namespace Yogurt.Arena
{
    public struct HealingPotionFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            HealingPotionConfig config = Query.Single<HealingPotionConfig>();

            ItemAspect item = await new ItemFactoryJob().Run(config, owner); 

            item.Config.UseJob.Run(item);
            
            return item;
        }
    }
}
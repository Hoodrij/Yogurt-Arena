using UnityEngine;

namespace Yogurt.Arena
{
    public struct HealingPotionFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            HealingPotionConfig config = Query.Single<Config>().HealingPotion;

            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseHealingPotionJob(), 
                EItemType.HealingPotion);
            item.Add(config);

            await item.Item.Job.Run(item);
            
            return item;
        }
    }
}
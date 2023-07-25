using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseHealingPotionJob : IItemUseJob
    {
        public async Awaitable Run(ItemAspect item)
        {
            HealingPotionConfig config = item.Get<HealingPotionConfig>();
            AgentAspect agentAspect = item.Owner.Owner;
            
            new ChangeHealthJob().Run(agentAspect.Entity, config.Amount);
        }
    }
}
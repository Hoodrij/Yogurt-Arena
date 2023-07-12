using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseHealingPotionJob : IItemUseJob
    {
        public async Awaitable Run(ItemAspect item)
        {
            HealingPotionData data = item.Get<HealingPotionData>();
            AgentAspect agentAspect = item.Owner.Owner;
            
            new HealJob().Run(agentAspect.Entity, data.Amount);
        }
    }
}
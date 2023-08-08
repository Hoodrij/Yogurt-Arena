using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseHealingPotionJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            HealingPotionConfig config = item.Get<HealingPotionConfig>();
            AgentAspect agentAspect = item.Owner.Owner;
            
            new ChangeHealthJob().Run(agentAspect.Entity, config.Amount);
        }
    }
}
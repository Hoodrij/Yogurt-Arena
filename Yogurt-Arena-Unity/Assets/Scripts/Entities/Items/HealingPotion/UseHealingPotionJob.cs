namespace Yogurt.Arena;

public struct UseHealingPotionJob : IItemUseJob
{
    public async UniTask Run(ItemAspect item)
    {
        HealingPotionConfig config = item.Get<HealingPotionConfig>();
        AgentAspect agentAspect = item.Owner.Value;
            
        new ChangeHealthJob().Run(agentAspect.Entity, config.Amount);
    }
}
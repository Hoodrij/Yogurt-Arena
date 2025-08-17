namespace Yogurt.Arena;

public struct CommonWeaponFactoryJob : IItemFactoryJob
{
    public async UniTask Run(ItemAspect item)
    {
        BattleState ownerBattleState = item.Owner.Value.BattleState;
        item.Add(ownerBattleState);

        if (item.TryGet(out WeaponClipConfig clipConfig) 
            && clipConfig.BulletsInClip > 0)
        {
            item.Add(new WeaponClipState
            {
                CurrentAmmo = clipConfig.BulletsInClip
            });
        }

        if (item.Has<ItemLifetimeConfig>())
        {
            new WeaponLifetimeJob().Run(item);
        }
            
        if (item.Has<TargetDetectionConfig>())
        {
            new CommonTargetDetectionJob().Run(item).Forget();
        }
    }
}
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CommonWeaponFactoryJob : IItemFactoryJob
    {
        public async UniTask Run(ItemAspect item)
        {
            BattleState ownerBattleState = item.Owner.Value.BattleState;
            item.Add(ownerBattleState);

            if (item.TryGet(out WeaponClipConfig clipConfig))
            {
                item.Add(new WeaponClipState
                {
                    CurrentAmmo = clipConfig.BulletsInClip
                });
            }

            if (item.TryGet(out ItemLifetimeConfig lifetimeConfig))
            {
                new WeaponLifetimeJob().Run(item);
            }
            
            if (item.TryGet(out TargetDetectionConfig targetDetectionConfig))
            {
                new CommonTargetDetectionJob().Run(item);
            }
        }
    }
}
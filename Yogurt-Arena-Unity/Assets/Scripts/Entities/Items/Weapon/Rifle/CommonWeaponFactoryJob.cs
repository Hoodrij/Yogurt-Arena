using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CommonWeaponFactoryJob : IItemFactoryJob
    {
        public async UniTask Run(ItemAspect item)
        {
            BattleState ownerBattleState = item.Owner.Owner.BattleState;
            item.Add(ownerBattleState);

            if (item.TryGet(out WeaponClipConfig clipConfig))
            {
                item.Add(new WeaponClipState
                {
                    CurrentAmmo = clipConfig.BulletsInClip
                });
            }
            
            new CommonTargetDetectionJob().Run(item);
        }
    }
}
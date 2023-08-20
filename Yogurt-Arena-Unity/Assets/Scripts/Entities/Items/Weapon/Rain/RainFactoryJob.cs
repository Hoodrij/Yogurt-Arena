using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainFactoryJob : IItemFactoryJob
    {
        public async UniTask Run(ItemAspect item)
        {
            item.Add(new WeaponClipState
            {
                CurrentAmmo = item.Get<WeaponClipConfig>().BulletsInClip
            });
            item.Add(item.Owner.Owner.BattleState);
            
            new CommonTargetDetectionJob().Run(item);
        }
    }
}
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CommonWeaponFactoryJob : IItemFactoryJob
    {
        public async UniTask Run(ItemAspect item)
        {
            BattleState ownerBattleState = item.Owner.Owner.BattleState;
            item.Add(ownerBattleState);
            
            new CommonTargetDetectionJob().Run(item);
        }
    }
}
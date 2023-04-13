using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseChargeJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item, Entity owner)
        {
            ChargeData data = item.Get<ChargeData>();

            while (item.Exist())
            {
                await WaitForActivation();
                await WaitForTarget();
                
                owner.Add<Kinematic>();
                await UniTask.Delay(data.Duration.ToSeconds());
                owner.Remove<Kinematic>();
                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
            
            async UniTask WaitForTarget()
            {
                AgentBattleState battleState = owner.Get<AgentBattleState>();
                await UniTask.WaitWhile(() => !battleState.Target.Exist);
            }
            
            async UniTask WaitForActivation()
            {
                await UniTask.WaitUntil(() => !owner.Has<Kinematic>());
            }
        }
    }
}
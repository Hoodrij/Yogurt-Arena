using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item, Entity owner)
        {
            RifleData data = item.Get<RifleData>();
            
            while (item.Exist())
            {
                await WaitForActivation();
                await WaitForTarget();
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new RifleBulletBehaviorJob().Run(bullet); 

                await UniTask.Delay(data.FireRate.ToSeconds());
            }
            
            
            async UniTask WaitForTarget()
            {
                AgentBattleState battleState = owner.Get<AgentBattleState>();
                await UniTask.WaitWhile(() => !battleState.Target.Exist);
            }
            
            async UniTask WaitForActivation()
            {
                await UniTask.WaitUntil(owner.Has<Active>);
            }
        }

    }
}
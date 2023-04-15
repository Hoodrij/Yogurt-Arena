using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item, AgentAspect owner)
        {
            RifleData data = item.Get<RifleData>();
            
            while (item.Exist())
            {
                await WaitForActivation();
                await WaitForTarget();
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new FireBulletJob().Run(bullet, GetDir());
                new RifleBulletBehaviorJob().Run(bullet);
                
                await UniTask.Delay(data.Cooldown.ToSeconds(), DelayType.Realtime);
            }
            
            
            async UniTask WaitForTarget()
            {
                AgentBattleState battleState = owner.BattleState;
                await UniTask.WaitWhile(() => !battleState.Target.Exist());
            }
            
            async UniTask WaitForActivation()
            {
                await UniTask.WaitUntil(() => !owner.Has<Kinematic>());
            }

            Vector3 GetDir()
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.Position.WithY(0) - owner.Body.Position.WithY(0))
                    .WithY(0).normalized;

                return dir;
            }
        }

    }
}
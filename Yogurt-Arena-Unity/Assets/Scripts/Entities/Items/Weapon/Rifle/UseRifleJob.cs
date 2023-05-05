using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponData data = item.Get<WeaponData>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RifleBulletBehaviorJob().Run(bullet);

                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
            
            
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.Position.WithY(0) - owner.Body.Position.WithY(0))
                    .WithY(0).normalized;

                return dir * bullet.Data.Speed;
            }
        }
    }
}
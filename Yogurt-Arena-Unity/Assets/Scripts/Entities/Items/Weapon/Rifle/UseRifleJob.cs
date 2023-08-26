using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponConfig config = item.Get<WeaponConfig>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                if (!item.Exist()) return;
                
                FireBullet();

                await new ReloadJob().Run(item);
            }

            return;


            async void FireBullet()
            {
                BulletAspect bullet = await new BulletFactoryJob().Run(config.Bullet, owner);
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RifleBulletBehaviorJob().Run(bullet);
            }
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.MiddlePoint - owner.Body.MiddlePoint).normalized;

                Vector3 velocity = dir * bullet.Config.Speed;
                velocity = new ApplyScatteringJob().Run(item, velocity);

                return velocity;
            }
        }
    }
}
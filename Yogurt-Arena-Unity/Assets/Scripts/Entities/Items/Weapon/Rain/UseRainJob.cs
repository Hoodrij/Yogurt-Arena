using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponData weaponData = item.Get<WeaponData>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);

                BulletAspect bullet = await new RainBulletFactoryJob().Run(weaponData.Bullet, item.Get<RainData>(), owner);

                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RainBulletBehaviorJob().Run(bullet.As<RainBulletAspect>());
                
                bool hasAmmoInClip = await new SpendAmmoJob().Run(item.As<WeaponWithClipAspect>());
                if (hasAmmoInClip)
                {
                    await UniTask.Delay(weaponData.Cooldown.ToSeconds());
                }
            }
            
            
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.Position.WithY(0) - owner.Body.Position.WithY(0))
                    .normalized
                    .WithY(5)
                    .normalized;

                dir = new ApplyScatteringJob().Run(item, dir);
                
                return dir * bullet.Data.Speed;
            }
        }
    }
}
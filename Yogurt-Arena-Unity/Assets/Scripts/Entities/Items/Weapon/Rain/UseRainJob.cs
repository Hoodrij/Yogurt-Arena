using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            AgentAspect owner = item.Item.Owner;
            WeaponData weaponData = item.Get<WeaponData>();

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(weaponData.Bullet, owner);
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RifleBulletBehaviorJob().Run(bullet);
                
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
                    .WithY(0).normalized;

                return dir * bullet.Data.Speed;
            }
        }
    }
}
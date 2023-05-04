using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            AgentAspect owner = item.Owner.Owner.As<AgentAspect>();
            WeaponData weaponData = item.Get<WeaponData>();

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(weaponData.Bullet, owner);
                bullet.Add(new BattleState
                {
                    Target = owner.BattleState.Target
                });
                bullet.Add(new OwnerState
                {
                    Owner = owner.Entity
                });
                
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RainBulletBehaviorJob().Run(bullet, item.Get<RainData>());
                
                bool hasAmmoInClip = await new SpendAmmoJob().Run(item.As<WeaponWithClipAspect>());
                if (hasAmmoInClip)
                {
                    await UniTask.Delay(weaponData.Cooldown.ToSeconds());
                }
            }
            
            
            Vector3 GetVelocity(BulletAspect bullet)
            {
                return Vector3.up * bullet.Data.Speed;
            }
        }
    }
}
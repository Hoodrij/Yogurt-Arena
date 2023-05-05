using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            AgentAspect owner = item.Owner;
            WeaponData weaponData = item.Get<WeaponData>();

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);

                BulletAspect bullet = await Factory();

                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RainBulletBehaviorJob().Run(bullet.As<RainBulletAspect>());
                
                bool hasAmmoInClip = await new SpendAmmoJob().Run(item.As<WeaponWithClipAspect>());
                if (hasAmmoInClip)
                {
                    await UniTask.Delay(weaponData.Cooldown.ToSeconds());
                }
            }


            async UniTask<BulletAspect> Factory()
            {
                BulletAspect bullet = await new BulletFactoryJob().Run(weaponData.Bullet, owner);
                bullet.Add(new BattleState
                {
                    Target = owner.BattleState.Target
                });
                bullet.Add(new OwnerState
                {
                    Owner = owner
                });
                bullet.Add(item.Get<RainData>().BulletData);

                return bullet;
            }
            Vector3 GetVelocity(BulletAspect bullet)
            {
                return Vector3.up * bullet.Data.Speed;
            }
        }
    }
}
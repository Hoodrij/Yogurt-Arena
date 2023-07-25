﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
    {
        public async Awaitable Run(ItemAspect item)
        {
            WeaponConfig weaponConfig = item.Get<WeaponConfig>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                if (!item.Exist()) return;

                BulletAspect bullet = await new RainBulletFactoryJob().Run(weaponConfig.Bullet, item.Get<RainConfig>(), owner);

                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RainBulletBehaviorJob().Run(bullet.As<RainBulletAspect>());
                
                bool hasAmmoInClip = await new SpendAmmoJob().Run(item.As<WeaponWithClipAspect>());
                if (hasAmmoInClip)
                {
                    await Wait.Seconds(weaponConfig.Cooldown);
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
                
                return dir * bullet.Config.Speed;
            }
        }
    }
}
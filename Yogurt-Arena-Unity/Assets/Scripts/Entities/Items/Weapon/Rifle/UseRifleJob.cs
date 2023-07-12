﻿using UnityEngine;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async Awaitable Run(ItemAspect item)
        {
            WeaponData data = item.Get<WeaponData>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                if (!item.Exist()) return;
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RifleBulletBehaviorJob().Run(bullet);

                await Wait.Seconds(data.Cooldown);
            }
            
            
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.Position.WithY(0) - owner.Body.Position.WithY(0))
                    .WithY(0).normalized;

                dir = new ApplyScatteringJob().Run(item, dir);

                return dir * bullet.Data.Speed;
            }
        }
    }
}
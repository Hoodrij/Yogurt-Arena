using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBehaviorJob
    {
        public async Awaitable Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            
            owner.Add<Kinematic>();
            new ChargeMoveOwnerJob().Run(bullet);
            new ChargeUpdateBulletPositionJob().Run(bullet);
            TryDealDamage();
            await Wait.Any(WaitForOwnerDeath(), WaitForLifeTime());
            
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);

            
            async Awaitable TryDealDamage()
            {
                int damage = bullet.Config.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async Awaitable WaitForOwnerDeath() => await Wait.While(() => owner.Exist());
            async Awaitable WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
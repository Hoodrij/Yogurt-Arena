using UnityEngine;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async Awaitable Run(BulletAspect bullet)
        {
            CollisionInfo collision = default;

            Awaitable collisionTask = DetectHit();
            new RifleMoveBulletJob().Run(bullet);
            await Wait.Any(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                new DealDamageJob().Run(collision.Entity, bullet.Config.Damage);
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
            }

            await new KillBulletJob().Run(bullet);
            return;


            async Awaitable DetectHit()
            {
                collision = await new WaitForBulletHitJob().Run(bullet);
            }
            async Awaitable WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
            }
        }
    }
}
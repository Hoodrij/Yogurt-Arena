using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainBulletBehaviorJob
    {
        public async Awaitable Run(RainBulletAspect rainBullet)
        { 
            BulletAspect bullet = rainBullet.BulletAspect;
            RainBulletConfig rainConfig = rainBullet.Config;
            CollisionInfo collision = default;
            
            Awaitable collisionTask = DetectHit();
            new UpdateRainTargetJob().Run(rainBullet);
            new RainMoveBulletJob().Run(rainBullet);

            await Wait.Any(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
                new DealAoeDamageJob().Run(rainBullet.Owner, collision.Position, rainConfig.Damage);
            }
            
            new SpawnExplosionJob().Run(rainConfig.ExplosionAsset, bullet.Body.Position, rainConfig.Damage.Radius);
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
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainBulletBehaviorJob
    {
        public async UniTask Run(RainBulletAspect rainBullet)
        { 
            BulletAspect bullet = rainBullet.BulletAspect;
            RainBulletConfig rainConfig = rainBullet.Config;
            CollisionInfo collision = default;
            
            UniTask collisionTask = DetectHit();
            new UpdateRainTargetJob().Run(rainBullet);
            new RainMoveBulletJob().Run(rainBullet);

            await Wait.Any(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
                new DealAoeDamageJob().Run(rainBullet.Owner, collision.Position, rainConfig.Explosion.Damage);
            }
            
            new SpawnExplosionJob().Run(rainConfig.Explosion, bullet.Body.Position).Forget();
            await new KillBulletJob().Run(bullet);
            return;


            async UniTask DetectHit()
            {
                collision = await new WaitForBulletHitJob().Run(bullet);
            }
            async UniTask WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
            }
        }
    }
}
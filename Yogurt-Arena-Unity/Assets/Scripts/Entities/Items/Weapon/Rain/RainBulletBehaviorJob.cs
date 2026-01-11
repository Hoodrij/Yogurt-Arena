namespace Yogurt.Arena;

public struct RainBulletBehaviorJob
{
    public async UniTask Run(RainBulletAspect rainBullet)
    { 
        BulletAspect bullet = rainBullet.BulletAspect;
        RainBulletConfig rainConfig = rainBullet.Config;
        CollisionInfo collision = default;
            
        Life collisionTask = DetectHit();
        new UpdateRainTargetJob().Run(rainBullet);
        new RainMoveBulletJob().Run(rainBullet);

        await collisionTask
            .Or(WaitForLifeTime());

        if (collision.IsValid)
        {
            bullet.Body.Position = bullet.View.transform.position = collision.Position;
            new DealAoeDamageJob().Run(rainBullet.Owner, collision.Position, rainConfig.Explosion.Damage);
        }
            
        new SpawnExplosionJob().Run(rainConfig.Explosion, bullet.Body.Position).Forget();
        await new KillBulletJob().Run(bullet);
        return;


        async Life DetectHit()
        {
            collision = await new WaitForBulletHitJob().Run(bullet);
        }
        async Life WaitForLifeTime()
        {
            await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
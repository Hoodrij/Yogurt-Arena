namespace Yogurt.Arena;

public struct RifleBulletBehaviorJob
{
    public async UniTask Run(BulletAspect bullet)
    {
        CollisionInfo collision = default;

        Life collisionTask = DetectHit();
        new RifleMoveBulletJob().Run(bullet);
        
        await collisionTask.Or(WaitForLifeTime());
            
        if (collision.IsValid)
        {
            new DealDamageJob().Run(collision.Entity, bullet.Config.Damage);
            bullet.Body.Position = bullet.View.transform.position = collision.Position;
        }
            
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
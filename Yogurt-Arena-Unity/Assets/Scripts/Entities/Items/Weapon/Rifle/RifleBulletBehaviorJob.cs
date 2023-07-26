﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async Awaitable Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            CollisionInfo collision = default;
            Awaitable collisionTask = DetectHit();
            Transform transform = bullet.View.transform;
            BodyState body = bullet.Body;
            float timePassed = 0;
            
            bullet.Run(MoveBullet);

            await Wait.Any(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                new DealDamageJob().Run(collision.Entity, bullet.Config.Damage);
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
            }

            await new KillBulletJob().Run(bullet);


            async void MoveBullet()
            {
                if (bullet.Has<Kinematic>())
                    return;
                
                Vector3 newPos = body.Position + body.Velocity * time;
                body.Position = transform.position = newPos;
                
                timePassed += time.Delta / bullet.Config.LifeTime;
                float speed = Mathf.Lerp(bullet.Config.Speed, 0, timePassed);
                body.Velocity = body.Velocity.normalized * speed;
            }
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
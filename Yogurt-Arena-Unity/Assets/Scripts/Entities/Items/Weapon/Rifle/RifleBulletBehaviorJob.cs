using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            CollisionInfo collision = default;
            UniTask collisionTask = DetectHit();
            MoveBullet();

            await UniTask.WhenAny(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                new DealDamageJob().Run(collision.Entity, bullet.Data.Damage);
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
            }

            await new KillBulletJob().Run(bullet);


            async UniTaskVoid MoveBullet()
            {
                Transform transform = bullet.View.transform;
                BodyState body = bullet.Body;
                float timePassed = 0;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    Vector3 newPos = body.Position + body.Velocity * time;
                    body.Position = transform.position = newPos;
                    
                    timePassed += time.Delta / bullet.Data.LifeTime;
                    float speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    body.Velocity = body.Velocity.normalized * speed;
                    
                    await UniTaskEx.Yield();
                }
            }
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
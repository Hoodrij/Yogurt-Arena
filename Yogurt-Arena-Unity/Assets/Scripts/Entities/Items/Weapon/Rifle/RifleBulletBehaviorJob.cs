using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            int damage = bullet.Data.Damage;
            UniTask<CollisionInfo> collisionTask = DetectHit();
            MoveBullet();

            await UniTask.WhenAny(collisionTask, WaitForLifeTime());

            if (collisionTask.TryGetResult(out var collisionInfo))
            {
                new DealDamageJob().Run(collisionInfo.Entity, damage);
                bullet.Body.Position = bullet.View.transform.position = collisionInfo.Position;
            }

            await new KillBulletJob().Run(bullet);


            async void MoveBullet()
            {
                Transform transform = bullet.View.transform;
                BodyState body = bullet.Body;
                float timePassed = 0;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    Vector3 newPos = body.Position + body.Velocity * Time.deltaTime;
                    body.Position = transform.position = newPos;
                    
                    timePassed += Time.deltaTime / bullet.Data.LifeTime;
                    float speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    body.Velocity = body.Velocity.normalized * speed;
                    
                    await UniTask.Yield();
                }
            }
            async UniTask<CollisionInfo> DetectHit()
            {
                return await new WaitForBulletHitJob().Run(bullet);
            }
            async UniTask WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
            }
        }
    }
}
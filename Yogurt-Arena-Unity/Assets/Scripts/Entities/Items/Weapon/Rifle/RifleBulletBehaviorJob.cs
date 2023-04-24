using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            MoveBullet();
            await WaitHitAndDealDamage();
            await new KillBulletJob().Run(bullet);


            async void MoveBullet()
            {
                Transform transform = bullet.View.transform;
                BodyState body = bullet.Body;
                float timePassed = 0;
                float speed = bullet.Data.Speed;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    await UniTask.Yield();
                    
                    Vector3 newPos = body.Position + body.Velocity * Time.deltaTime;
                    body.Position = transform.position = newPos;
                    
                    timePassed += Time.deltaTime / bullet.Data.LifeTime;
                    speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    body.Velocity = body.Velocity.normalized * speed;
                }
            }
            async UniTask<CollisionInfo> WaitForHit() => await new WaitForBulletHitJob().Run(bullet);
            async UniTask<CollisionInfo> WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
                return default;
            }
            async Task WaitHitAndDealDamage()
            {
                int damage = bullet.Data.Damage;
                CollisionInfo collisionInfo = await UniTaskEx.WhenAny(WaitForHit, WaitForLifeTime);
                if (collisionInfo.IsValid)
                {
                    new DealDamageJob().Run(collisionInfo.Entity, damage);
                    bullet.Body.Position = bullet.View.transform.position = collisionInfo.Position;
                }
            }
        }
    }
}
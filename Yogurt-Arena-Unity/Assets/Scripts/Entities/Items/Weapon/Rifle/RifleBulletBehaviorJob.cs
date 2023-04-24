using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

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
                float timePassed = 0;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    await UniTask.Yield();
                    
                    timePassed += Time.deltaTime / bullet.Data.LifeTime;
                    float speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    
                    Vector3 velocity = transform.forward * speed * Time.deltaTime;
                    Vector3 newPos = transform.position + velocity;

                    transform.position = bullet.Body.Position = bullet.Body.Destination = newPos;
                    bullet.Body.Velocity = velocity;
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
                    bullet.View.transform.position = collisionInfo.Position;
                    bullet.Add<Kinematic>();
                }
            }
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet, RainData rainData)
        {
            CollisionInfo collision = default;
            UniTask collisionTask = DetectHit();
            new UpdateRainTargetJob().Run(bullet);
            MoveBullet();

            await UniTask.WhenAny(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                new DealDamageJob().Run(collision.Entity, bullet.Data.Damage);
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
            }

            await new KillBulletJob().Run(bullet);


            async void MoveBullet()
            {
                BattleState battleState = bullet.Get<BattleState>();
                Transform transform = bullet.View.transform;
                BodyState body = bullet.Body;
                float timePassed = 0;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    AgentAspect target = battleState.Target;
                    Vector3 newPos = body.Position + body.Velocity * Time.deltaTime;
                    body.Position = transform.position = newPos;
                    
                    Vector3 dirToTarget = (target.Body.Position - body.Position).normalized;
                    Vector3 neededVelocity = dirToTarget * body.Velocity.magnitude;

                    neededVelocity = Vector3.RotateTowards(body.Velocity, neededVelocity, rainData.ChaseValue1 * Time.deltaTime, 0);
                    body.Velocity = Vector3.Lerp(body.Velocity, neededVelocity, rainData.ChaseValue2 * Time.deltaTime);

                    body.Velocity += rainData.Gravity * Time.deltaTime;
                    
                    await UniTask.Yield();
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
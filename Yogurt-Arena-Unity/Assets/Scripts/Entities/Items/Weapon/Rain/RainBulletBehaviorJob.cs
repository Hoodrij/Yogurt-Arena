using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainBulletBehaviorJob
    {
        public async UniTask Run(RainBulletAspect rainBullet)
        { 
            Time time = Query.Single<Time>();
            BulletAspect bullet = rainBullet.BulletAspect;
            RainBulletData rainData = rainBullet.Data;
            CollisionInfo collision = default;
            
            UniTask collisionTask = DetectHit();
            new UpdateRainTargetJob().Run(rainBullet);
            MoveBullet();

            await Wait.Any(collisionTask, WaitForLifeTime());

            if (collision.IsValid)
            {
                bullet.Body.Position = bullet.View.transform.position = collision.Position;
                new DealAoeDamageJob().Run(rainBullet.Owner, collision.Position, rainData.Damage);
            }
            
            new SpawnExplosionJob().Run(rainData.ExplosionAsset, bullet.Body.Position, rainData.Damage.Radius);
            await new KillBulletJob().Run(bullet);


            async UniTask MoveBullet()
            {
                BattleState battleState = bullet.Get<BattleState>();
                Transform transform = bullet.View.transform;
                BodyState body = bullet.Body;
                float timePassed = 0;

                while (bullet.Exist() && !bullet.Has<Kinematic>())
                {
                    Vector3 newPos = body.Position + body.Velocity * time;
                    body.Position = transform.position = newPos;
                    body.Velocity += rainData.Gravity * time;
                    
                    AgentAspect target = battleState.Target;
                    if (target.Exist() && body.Velocity.y < 0)
                    {
                        Vector3 dirToTarget = (target.Body.Position - body.Position).normalized;
                        Vector3 neededVelocity = dirToTarget * body.Velocity.magnitude;
                        neededVelocity = Vector3.RotateTowards(body.Velocity, neededVelocity, rainData.BulletRotationSpeed * time, 0);
                        body.Velocity = Vector3.Lerp(body.Velocity, neededVelocity, rainData.BulletSpeedChangeCoef * time);
                    }

                    await Wait.Update();
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
﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainMoveBulletJob
    {
        public void Run(RainBulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            RainBulletData rainData = bullet.Data;
            
            bullet.Run(MoveBullet);
            
            
            void MoveBullet()
            {
                if (bullet.Has<Kinematic>())
                    return;
                
                BattleState battleState = bullet.Get<BattleState>();
                Transform transform = bullet.BulletAspect.View.transform;
                BodyState body = bullet.BulletAspect.Body;
                
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
            }
        }
    }
}
namespace Yogurt.Arena;

public struct RainMoveBulletJob
{
    public void Run(RainBulletAspect bullet)
    {
        Time time = Query.Single<Time>();
        RainBulletConfig rainConfig = bullet.Config;
            
        bullet.Run(MoveBullet);
        return;


        void MoveBullet()
        {
            if (bullet.Has<Kinematic>())
                return;
                
            BattleState battleState = bullet.Get<BattleState>();
            Transform transform = bullet.BulletAspect.View.transform;
            BodyState body = bullet.BulletAspect.Body;
                
            Vector3 newPos = body.Position + body.Velocity * time;
            body.Position = transform.position = newPos;
            body.Velocity += rainConfig.Gravity * time;
                
            AgentAspect target = battleState.Target;
            if (target.Exist() && body.Velocity.y < 0)
            {
                Vector3 dirToTarget = (target.Body.Position - body.Position).normalized;
                Vector3 neededVelocity = dirToTarget * body.Velocity.magnitude;
                neededVelocity = Vector3.RotateTowards(body.Velocity, neededVelocity, rainConfig.BulletRotationSpeed * time, 0);
                body.Velocity = Vector3.Lerp(body.Velocity, neededVelocity, rainConfig.BulletSpeedChangeCoef * time);
            }
        }
    }
}
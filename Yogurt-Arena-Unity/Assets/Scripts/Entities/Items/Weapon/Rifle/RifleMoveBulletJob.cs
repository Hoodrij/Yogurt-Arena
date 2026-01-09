namespace Yogurt.Arena;

public struct RifleMoveBulletJob
{
    public void Run(BulletAspect bullet)
    {
        Transform transform = bullet.View.transform;
        ref BodyState body = ref bullet.Body;
        float timePassed = 0;
        float startingSpeed = body.Velocity.magnitude;
            
        bullet.Run(MoveBullet);
        return;


        void MoveBullet()
        {
            if (bullet.Has<Kinematic>())
                return;
                
            ref Time time = ref Query.Single<Time>();
            ref BodyState body = ref bullet.Body;
            Vector3 newPos = body.Position + body.Velocity * time;
            body.Position = transform.position = newPos;
                
            timePassed += time.Delta / bullet.Config.LifeTime;
            float speed = Mathf.Lerp(startingSpeed, 0, timePassed);
            body.Velocity = body.Velocity.normalized * speed;
        }
    }
}
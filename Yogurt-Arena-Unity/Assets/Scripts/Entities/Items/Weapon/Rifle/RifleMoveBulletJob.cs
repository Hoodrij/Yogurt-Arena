namespace Yogurt.Arena
{
    public struct RifleMoveBulletJob
    {
        public void Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            Transform transform = bullet.View.transform;
            BodyState body = bullet.Body;
            float timePassed = 0;
            float startingSpeed = body.Velocity.magnitude;
            
            bullet.Run(MoveBullet);
            return;


            void MoveBullet()
            {
                if (bullet.Has<Kinematic>())
                    return;
                
                Vector3 newPos = body.Position + body.Velocity * time;
                body.Position = transform.position = newPos;
                
                timePassed += time.Delta / bullet.Config.LifeTime;
                float speed = Mathf.Lerp(startingSpeed, 0, timePassed);
                body.Velocity = body.Velocity.normalized * speed;
            }
        }
    }
}
using UnityEngine;

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
            
            bullet.Run(MoveBullet);
            
            
            void MoveBullet()
            {
                if (bullet.Has<Kinematic>())
                    return;
                
                Vector3 newPos = body.Position + body.Velocity * time;
                body.Position = transform.position = newPos;
                
                timePassed += time.Delta / bullet.Config.LifeTime;
                float speed = Mathf.Lerp(bullet.Config.Speed, 0, timePassed);
                body.Velocity = body.Velocity.normalized * speed;
            }
        }
    }
}
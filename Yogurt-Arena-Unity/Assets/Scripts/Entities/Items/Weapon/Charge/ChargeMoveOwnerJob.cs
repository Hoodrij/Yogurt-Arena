using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeMoveOwnerJob
    {
        public void Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            AgentAspect owner = bullet.State.Owner;
            Transform transform = owner.View.transform;
            BodyState body = owner.Body;
            
            float timePassed = 0;
            body.Velocity = transform.forward * bullet.Config.Speed;
            
            bullet.Run(MoveOwner);
            
            
            async void MoveOwner()
            {
                if (!owner.Has<Kinematic>())
                    return;
                
                float deltaTime = time.Delta;
                Vector3 newPos = body.Position + body.Velocity * time.Scale;
                
                timePassed += deltaTime / bullet.Config.LifeTime;
                float speed = Mathf.Lerp(bullet.Config.Speed, 0, timePassed);
                body.Velocity = body.Velocity.normalized * speed;
                // required for collision detection
                bullet.Body.Velocity = body.Velocity;
                
                if (NavMesh.SamplePosition(newPos, out var hit, 1, NavMesh.AllAreas))
                {
                    body.Position = transform.position = body.Destination = hit.position;
                }
            }
        }
    }
}
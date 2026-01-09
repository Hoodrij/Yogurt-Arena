namespace Yogurt.Arena;

public struct ChargerWeaponMoveOwnerJob
{
    public void Run(BulletAspect bullet)
    {
        AgentAspect owner = bullet.Owner.Value;
        ref BodyState body = ref owner.Body;
            
        float timePassed = 0;
        body.Velocity = body.Forward * bullet.Config.Speed;
            
        bullet.Run(MoveOwner);
        return;


        void MoveOwner()
        {
            if (!owner.Has<Kinematic>())
                return;
                
            ref Time time = ref Query.Single<Time>();
            ref BodyState body = ref owner.Body;
            float deltaTime = time.Delta;
            Vector3 newPos = body.Position + body.Velocity * time.Scale;
                
            timePassed += deltaTime / bullet.Config.LifeTime;
            float speed = Mathf.Lerp(bullet.Config.Speed, 0, timePassed);
            body.Velocity = body.Velocity.normalized * speed;
            // required for collision detection
            bullet.Body.Velocity = body.Velocity;
                
            if (NavMesh.SamplePosition(newPos, out var hit, 1, NavMesh.AllAreas))
            {
                body.Position = body.Destination = hit.position;
            }
        }
    }
}
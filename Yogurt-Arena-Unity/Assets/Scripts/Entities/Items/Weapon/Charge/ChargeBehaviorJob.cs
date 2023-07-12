﻿using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBehaviorJob
    {
        public async Awaitable Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();
            
            new ChargeUpdateBulletPositionJob().Run(bullet);
            TryDealDamage();
            MoveOwner();
            await Wait.Any(WaitForOwnerDeath(), WaitForLifeTime());
            
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);


            async Awaitable MoveOwner()
            {
                Transform transform = owner.View.transform;
                BodyState body = owner.Body;
                float timePassed = 0;
                body.Velocity = transform.forward * bullet.Data.Speed;
                
                while (owner.Exist() && owner.Has<Kinematic>())
                {
                    float deltaTime = time.Delta;
                    Vector3 newPos = body.Position + body.Velocity * time.Scale;
                    NavMesh.SamplePosition(newPos, out var hit, 10, NavMesh.AllAreas);
                    body.Position = transform.position = body.Destination = hit.position;
                    
                    timePassed += deltaTime / bullet.Data.LifeTime;
                    float speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    body.Velocity = body.Velocity.normalized * speed;
                    // required for collision detection
                    bullet.Body.Velocity = body.Velocity;

                    await Wait.Update();
                }
            }
            async Awaitable TryDealDamage()
            {
                int damage = bullet.Data.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async Awaitable WaitForOwnerDeath() => await Wait.While(() => owner.Exist());
            async Awaitable WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
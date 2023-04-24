using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();
            
            MoveOwner();
            TryDealDamage();
            new ChargeUpdateBulletPositionJob().Run(bullet);
            await UniTask.WhenAny(WaitForOwnerDeath(), WaitForLifeTime());
            
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);


            async void MoveOwner()
            {
                Transform transform = owner.View.transform;
                float timePassed = 0;

                while (owner.Exist())
                {
                    await UniTask.Yield();
                    
                    timePassed += Time.deltaTime / bullet.Data.LifeTime;
                    float speed = Mathf.Lerp(bullet.Data.Speed, 0, timePassed);
                    
                    Vector3 velocity = transform.forward * speed * Time.deltaTime;
                    Vector3 newPos = transform.position + velocity;
                    NavMesh.SamplePosition(newPos, out var hit, 10, NavMesh.AllAreas);
                    
                    transform.position = owner.Body.Position = owner.Body.Destination = hit.position;
                    bullet.Body.Velocity = velocity;
                }
            }
            async void TryDealDamage()
            {
                int damage = bullet.Data.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async UniTask WaitForOwnerDeath() => await UniTask.WaitWhile(() => owner.Exist());
            async UniTask WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
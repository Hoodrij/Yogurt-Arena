using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();

            KillBulletWhenOwnerIsDead();
            TryDealDamage();
            UpdateOwner();
            await new WaitForBulletLiteTimeJob().Run(bullet);
            
            owner.Remove<Kinematic>();
            bullet.Kill();
            

            async void TryDealDamage()
            {
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, bullet.Data.Damage);
            }
            
            async void UpdateOwner()
            {
                Transform ownerTransform = owner.View.transform;
                
                while (bullet.Exist())
                {
                    NavMesh.SamplePosition(bullet.View.transform.position, out var attackPositionHit, 100, NavMesh.AllAreas);
                    ownerTransform.position = owner.Body.Position = owner.Body.Destination = attackPositionHit.position;
                    await UniTask.Yield();
                }
            }

            async void KillBulletWhenOwnerIsDead()
            {
                await UniTask.WaitWhile(() => owner.Exist());
                if (bullet.Exist())
                    bullet.Kill();
            }
        }
    }
}
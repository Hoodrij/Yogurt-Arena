using Cysharp.Threading.Tasks;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();
            TryDealDamage();
            bullet.Run(UpdateOwnerPosition);

            await UniTask.WhenAny(WaitForOwnerDeath(), WaitForLifeTime());
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);

            
            async void TryDealDamage()
            {
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                if (bullet.Exist())
                {
                    new DealDamageJob().Run(collisionInfo.Entity, bullet.Data.Damage);
                }
            }
            async void UpdateOwnerPosition()
            {
                if (owner.Exist())
                {
                    NavMesh.SamplePosition(bullet.View.transform.position, out var attackPositionHit, 100, NavMesh.AllAreas);
                    owner.View.transform.position = owner.Body.Position = owner.Body.Destination = attackPositionHit.position;
                }
            }
            async UniTask WaitForOwnerDeath()
            {
                await UniTask.WaitWhile(() => owner.Exist());
            }
            async UniTask WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
            }
        }
    }
}
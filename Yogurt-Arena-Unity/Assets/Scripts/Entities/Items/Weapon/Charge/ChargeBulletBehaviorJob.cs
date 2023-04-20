using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ChargeBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();
            TryDealDamage();
            new ChargeUpdateOwnerPositionJob().Run(bullet);

            await UniTask.WhenAny(WaitForOwnerDeath(), WaitForLifeTime(), WaitForEnvironmentHit());

            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);

            
            async void TryDealDamage()
            {
                int damage = bullet.Data.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async UniTask WaitForOwnerDeath() => await UniTask.WaitWhile(() => owner.Exist());
            async UniTask WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
            async UniTask WaitForEnvironmentHit() => await new WaitForBulletNavMeshBoundHitJob().Run(bullet);
        }
    }
}
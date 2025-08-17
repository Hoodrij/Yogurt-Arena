using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ChargeBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.Owner.Value;
            
            owner.Add(new Kinematic());
            new ChargeMoveOwnerJob().Run(bullet);
            new ChargeUpdateBulletPositionJob().Run(bullet);
            TryDealDamage().Forget();
            await Wait.Any(WaitForOwnerDeath(), WaitForLifeTime());
            
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);
            return;


            async UniTask TryDealDamage()
            {
                int damage = bullet.Config.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async UniTask WaitForOwnerDeath() => await Wait.While(() => owner.Exist());
            async UniTask WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
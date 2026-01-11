namespace Yogurt.Arena;

public struct ChargerWeaponBehaviorJob
{
    public async UniTask Run(BulletAspect bullet)
    {
        AgentAspect owner = bullet.Owner.Value;

        owner.Add(new Kinematic());
        new ChargerWeaponMoveOwnerJob().Run(bullet);
        new ChargerWeaponMoveBulletJob().Run(bullet);
        TryDealDamage().Forget();
        await WaitForOwnerDeath()
            .Or(WaitForLifeTime());
            
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
        async Life WaitForOwnerDeath() => await owner.Life();
        async Life WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
    }
}
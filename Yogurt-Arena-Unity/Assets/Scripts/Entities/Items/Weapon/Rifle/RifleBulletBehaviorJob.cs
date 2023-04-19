using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            int damage = bullet.Data.Damage;
            CollisionInfo collisionInfo = await UniTaskEx.WhenAny(WaitForHit, WaitForLifeTime);
            if (collisionInfo.IsValid)
            {
                new DealDamageJob().Run(collisionInfo.Entity, damage);
                bullet.View.transform.position = collisionInfo.Position;
            }
            
            await new KillBulletJob().Run(bullet);


            async UniTask<CollisionInfo> WaitForHit()
            {
                return await new WaitForBulletHitJob().Run(bullet);
            }

            async UniTask<CollisionInfo> WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
                return default;
            }
        }
    }
}
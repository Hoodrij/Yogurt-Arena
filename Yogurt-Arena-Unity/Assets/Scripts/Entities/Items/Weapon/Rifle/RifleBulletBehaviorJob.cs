using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            CollisionInfo collisionInfo = await UniTaskEx.WhenAny(WaitForHit, WaitForLifeTime);
            new DealDamageJob().Run(collisionInfo.Entity, bullet.Data.Damage);
            
            bullet.View.transform.position = collisionInfo.Position;
            await new KillBulletJob().Run(bullet);


            async UniTask<CollisionInfo> WaitForHit()
            {
                return await new WaitForBulletHitJob().Run(bullet);
            }

            async UniTask<CollisionInfo> WaitForLifeTime()
            {
                await new WaitForBulletLiteTimeJob().Run(bullet);
                return new CollisionInfo
                {
                    Position = bullet.Position
                };
            }
        }
    }
}
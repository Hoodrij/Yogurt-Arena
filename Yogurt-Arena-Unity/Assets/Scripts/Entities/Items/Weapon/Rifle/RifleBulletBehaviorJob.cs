using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            await UniTask.WhenAny(WaitForHit(bullet), WaitForLifeTime(bullet));
            bullet.Kill();
        }
        
        static async UniTask WaitForHit(BulletAspect bullet)
        {
            CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
        }

        static async UniTask WaitForLifeTime(BulletAspect bullet)
        {
            await new WaitForBulletLiteTimeJob().Run(bullet);
        }

    }
}
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RifleBulletBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            CollisionInfo collisionInfo = await UniTaskEx.WhenAny(WaitForHit(bullet), WaitForLifeTime(bullet));
            bullet.State.RigidBody.isKinematic = true;
            bullet.View.transform.position = collisionInfo.Position;

            await UniTask.Delay(0.3f.Seconds());
            bullet.Kill();
        }
        
        static async UniTask<CollisionInfo> WaitForHit(BulletAspect bullet)
        {
            return await new WaitForBulletHitJob().Run(bullet);
        }

        static async UniTask<CollisionInfo> WaitForLifeTime(BulletAspect bullet)
        {
            await new WaitForBulletLiteTimeJob().Run(bullet);
            return new CollisionInfo
            {
                Position = bullet.Position
            };
        }

    }
}
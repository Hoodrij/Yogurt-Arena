using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForBulletHitJob
    {
        public async UniTask<CollisionInfo> Run(BulletAspect bullet)
        {
            while (true)
            {
                GameObject hitGO = await bullet.CollisionDetector.WaitForCollision();
                if (!hitGO.TryGetComponent(out EntityLink link)) 
                    continue;
                if (link.Entity == bullet.State.Owner)
                    continue;

                return new CollisionInfo
                {
                    Entity = link,
                    Position = bullet.Position
                };
            }
            
            return default;
        }
    }
}
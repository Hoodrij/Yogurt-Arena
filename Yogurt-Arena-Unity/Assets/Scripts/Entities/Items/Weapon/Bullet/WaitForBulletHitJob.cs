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
                RaycastHit hit = await bullet.CollisionDetector.CollisionEvent;
                if (!hit.transform.TryGetComponent(out EntityLink link)) 
                    continue;
                if (link.Entity == bullet.State.Owner)
                    continue;

                return new CollisionInfo
                {
                    Entity = link,
                    Position = bullet.Position
                };
            }
        }
    }
}
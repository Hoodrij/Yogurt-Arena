using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForBulletHitJob
    {
        public async UniTask<CollisionInfo> Run(BulletAspect bullet)
        {
            RaycastHit[] hits = new RaycastHit[3];
            
            while (bullet.Exist())
            {
                int hitsCount = GetHits(bullet, ref hits);
                for (var i = 0; i < hitsCount; i++)
                {
                    RaycastHit hit = hits[i];
                    if (!hit.transform.TryGetComponent(out EntityLink link)) 
                        continue;
                    if (link.Entity == bullet.State.Owner.Entity)
                        continue;

                    return new CollisionInfo
                    {
                        IsValid = true,
                        Position = hit.point == Vector3.zero ? bullet.Body.Position : hit.point,
                        Entity = link.Entity
                    };
                }

                await UniTask.Yield();
            };

            return default;
        }

        private static int GetHits(BulletAspect bullet, ref RaycastHit[] result)
        {
            Vector3 velocity = bullet.Body.Velocity;
            Vector3 dir = velocity.normalized;
            float speed = velocity.magnitude * Time.deltaTime;

            int hitsCount = Physics.SphereCastNonAlloc(bullet.Body.Position, bullet.Data.Radius, dir, result, speed, bullet.Data.HitMask);
            return hitsCount;
        }
    }
}
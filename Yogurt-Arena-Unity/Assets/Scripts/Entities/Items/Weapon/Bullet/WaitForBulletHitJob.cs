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
                await UniTask.Yield();
                
                int hitsCount = GetHits();
                for (var i = 0; i < hitsCount; i++)
                {
                    RaycastHit hit = hits[i];
                    if (hit.transform.TryGetComponent(out EntityLink link))
                    {
                        if (link.Entity == bullet.State.Owner.Entity)
                            continue;
                    }

                    return new CollisionInfo
                    {
                        IsValid = true,
                        Position = hit.point == Vector3.zero ? bullet.Body.Position : hit.point,
                        Entity = link ? link.Entity : default
                    };
                }
            };

            return default;
            
            
            int GetHits()
            {
                Vector3 velocity = bullet.Body.Velocity;
                Vector3 dir = velocity.normalized;
                float speed = velocity.magnitude * Time.deltaTime;

                int hitsCount = Physics.SphereCastNonAlloc(bullet.Body.Position, bullet.Data.Radius, dir, hits, speed, bullet.Data.HitMask);
                return hitsCount;
            }
        }

    }
}
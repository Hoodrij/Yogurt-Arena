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
                        Position = hit.point == Vector3.zero ? bullet.Position : hit.point,
                        Entity = link.Entity
                    };
                }

                await UniTask.Yield();
            };

            return default;
        }

        private static int GetHits(BulletAspect bullet, ref RaycastHit[] result)
        {
            float radius = bullet.State.Collider.radius;
            Vector3 velocity = bullet.State.RigidBody.velocity;
            Vector3 dir = velocity.normalized;
            float speed = velocity.magnitude * Time.deltaTime;

            int hitsCount = Physics.SphereCastNonAlloc(bullet.Position, radius, dir, result, speed, bullet.Data.HitMask);
            return hitsCount;
        }
    }
}
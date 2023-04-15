using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForBulletHitJob
    {
        public async UniTask<CollisionInfo> Run(BulletAspect bullet)
        {
            RaycastHit[] hits = new RaycastHit[10];
            
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
                        Position = hit.point,
                        Entity = link.Entity
                    };
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            };

            return default;
        }

        private static int GetHits(BulletAspect bullet, ref RaycastHit[] result)
        {
            Rigidbody body = bullet.State.RigidBody;
            float radius = bullet.State.Collider.radius;
            
            Vector3 moveDir = body.velocity.normalized;
            float moveSpeed = body.velocity.magnitude * Time.fixedDeltaTime;

            int hitsCount = Physics.SphereCastNonAlloc(bullet.Position, radius, moveDir, result, moveSpeed, bullet.Data.HitMask);
            return hitsCount;
        }
    }
}
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
                RaycastHit hit = await WaitForCollision(bullet);
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

        private static async UniTask<RaycastHit> WaitForCollision(BulletAspect bullet)
        {
            RaycastHit hit = default;
            Rigidbody body = bullet.State.RigidBody;
            float radius = bullet.State.Collider.radius;

            await UniTask.WaitUntil(() =>
            {
                Vector3 moveDir = body.velocity.normalized;
                float moveSpeed = body.velocity.magnitude * Time.fixedDeltaTime;

                return Physics.SphereCast(body.position, radius, moveDir, out hit, moveSpeed, bullet.Data.HitMask);
            });
            
            return hit;
        }
    }
}
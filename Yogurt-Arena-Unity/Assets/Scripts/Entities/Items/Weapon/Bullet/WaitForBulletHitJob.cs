using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForBulletHitJob
    {
        public async UniTask<CollisionInfo> Run(BulletAspect bullet)
        {
            Time time = Query.Single<Time>();
            RaycastHit[] hits = new RaycastHit[3];
            CollisionInfo result = default;

            await Wait.Until(HasHit, bullet.Life());

            return result;


            bool HasHit()
            {
                int hitsCount = GetHits();
                for (var i = 0; i < hitsCount; i++)
                {
                    RaycastHit hit = hits[i];
                    Entity entityHit = hit.GetEntity();

                    if (entityHit == bullet.Owner.Value.Entity)
                        continue;

                    result = new CollisionInfo
                    {
                        IsValid = true,
                        Position = hit.point == Vector3.zero ? bullet.Body.Position : hit.point,
                        Entity = entityHit
                    };
                    return true;
                }

                return false;
            }
            int GetHits()
            {
                Vector3 velocity = bullet.Body.Velocity;
                Vector3 dir = velocity.normalized;
                float speed = velocity.magnitude * time;

                int hitsCount = Physics.SphereCastNonAlloc(bullet.Body.Position, bullet.Config.Radius, dir, hits, speed, bullet.Config.CollisionMask);
                return hitsCount;
            }
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct WaitForBulletNavMeshBoundHitJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            while (bullet.Exist())
            {
                Vector3 nextPosition = bullet.GetNextPosition();
                if (!NavMesh.SamplePosition(nextPosition, out var hit, 1f, NavMesh.AllAreas))
                {
                    return;
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
        }
    }
}
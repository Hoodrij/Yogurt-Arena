using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct GetFreeSpawnPointJob
    {
        public async UniTask<Vector3> Run(OvermindAspect overmind)
        {
            NavMeshSurface level = Query.Single<Location>().NavSurface;
            PlayerAspect player = Query.Single<PlayerAspect>();

            while (overmind.Exist())
            {
                Vector3 randomPoint = level.navMeshData.sourceBounds.GetRandomPoint().WithY(0);
                NavMesh.SamplePosition(randomPoint, out var hit, 100, NavMesh.AllAreas);
                Vector3 randomPointOnNavMesh = hit.position;

                if (IsPlayerFarEnough(randomPointOnNavMesh))
                {
                    return randomPointOnNavMesh;
                }
                
                await Wait.Update(overmind.Entity);
            }

            return default;


            bool IsPlayerFarEnough(Vector3 randomPoint)
            {
                if (!player.Exist())
                    return true;
                
                Vector3 playerPosition = player.Agent.Body.Position;
                float distanceToPlayer = (playerPosition - randomPoint).magnitude;

                return distanceToPlayer > overmind.Config.MinRangeToPlayer;
            }
        }
    }
}
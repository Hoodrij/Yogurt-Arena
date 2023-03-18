using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct OvermindFactoryJob
    {
        public async UniTask Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();
            
            for (int i = 0; i < data.Overmind.EnemiesCount; i++)
            {
                AgentAspect agent = await new AgentFactoryJob().Run(assets.Enemy_1);
                Vector3 spawnPoint = GetFreeSpawnPoint();
                agent.State.Position = spawnPoint;
                agent.State.Destination = spawnPoint;
            }
        }

        private Vector3 GetFreeSpawnPoint()
        {
            NavMeshSurface level = Query.Single<Level>().NavSurface;
            Vector3 randomPoint = level.navMeshData.sourceBounds.GetRandomPoint().WithY(0);

            NavMesh.SamplePosition(randomPoint, out var hit, 100, NavMesh.AllAreas);

            return hit.position;
        }
    }
}
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
                AgentAspect agent = await new AgentFactoryJob().Run(assets.Enemy_1, Team.Red);
                Vector3 spawnPoint = GetFreeSpawnPoint();
                agent.Body.Position = spawnPoint;
                agent.Body.Destination = spawnPoint;
                
                // agent.Items.Add(await new RifleFactoryJob().Run(agent));
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
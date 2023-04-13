using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct SpawnWaveJob
    {
        public async UniTask Run(OvermindAspect overmind)
        {
            AgentData data = Query.Single<Data>().ChargeEnemy;
            
            int agentsCount = overmind.Data.WaveAgentsCount.GetRandom();

            for (int i = 0; i < agentsCount; i++)
            {
                Vector3 spawnPoint = GetFreeSpawnPoint();
                AgentAspect agent = await new AgentSpawnJob().Run(data, Team.Red, spawnPoint);
                agent.Items.Add(await new ChargeFactoryJob().Run(agent));

                overmind.State.AddAgent(agent);
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
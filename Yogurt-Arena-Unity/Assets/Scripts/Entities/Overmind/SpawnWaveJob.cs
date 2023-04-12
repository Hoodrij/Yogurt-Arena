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
                AgentAspect agent = await new AgentFactoryJob().Run(data, Team.Red);
                Vector3 spawnPoint = GetFreeSpawnPoint();
                agent.Body.Position = spawnPoint;
                agent.Body.Destination = spawnPoint;
                agent.View.transform.position = agent.Body.Position;
                
                // agent.Items.Add(await new RifleFactoryJob().Run(agent));
                
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
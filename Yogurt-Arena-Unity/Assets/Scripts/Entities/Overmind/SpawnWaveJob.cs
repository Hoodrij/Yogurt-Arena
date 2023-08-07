using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct SpawnWaveJob
    {
        public async Awaitable Run(OvermindAspect overmind)
        {
            AgentConfig config = Query.Single<Config>().ChargeEnemy;
            
            int agentsCount = overmind.Config.WaveAgentsCount.GetRandom();

            for (int i = 0; i < agentsCount; i++)
            {
                Vector3 spawnPoint = GetFreeSpawnPoint();
                AgentAspect agent = await new AgentSpawnJob().Run(config, Team.Red, spawnPoint);
                await new ChargeFactoryJob().Run(agent);

                overmind.State.AddAgent(agent);
                await Wait.Seconds(0.5f);
            }

            return;


            Vector3 GetFreeSpawnPoint()
            {
                NavMeshSurface level = Query.Single<Level>().NavSurface;
                Vector3 randomPoint = level.navMeshData.sourceBounds.GetRandomPoint().WithY(0);

                NavMesh.SamplePosition(randomPoint, out var hit, 100, NavMesh.AllAreas);

                return hit.position;
            }
        }
    }
}
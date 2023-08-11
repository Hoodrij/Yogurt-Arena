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
            int agentsCount = overmind.Config.WaveAgentsCount.GetRandom();

            for (int i = 0; i < agentsCount; i++)
            {
                Vector3 spawnPoint = GetFreeSpawnPoint();
                AgentConfig config = new GetAgentConfigJob().Run(TeamType.Red);
                
                AgentAspect agent = await new AgentSpawnJob().Run(config, spawnPoint);
                IItemFactoryJob weaponFactory = new GetItemFactoryJob().Run(config.Weapon);
                weaponFactory.Run(agent);

                new SpawnWorldHealthWidget().Run(agent);

                overmind.State.KeepAgent(agent);
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
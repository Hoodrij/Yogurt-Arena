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
                Vector3 spawnPoint = await new GetFreeSpawnPointJob().Run(overmind);
                AgentConfig config = new GetAgentConfigJob().Run(TeamType.Red, overmind.Config.AvailableTypes);
                
                AgentAspect agent = await new AgentSpawnJob().Run(config, spawnPoint);
                // new SpawnWorldHealthWidget().Run(agent);

                overmind.State.KeepAgent(agent);
                await Wait.Seconds(0.5f, overmind.Entity);
            }
        }
    }
}
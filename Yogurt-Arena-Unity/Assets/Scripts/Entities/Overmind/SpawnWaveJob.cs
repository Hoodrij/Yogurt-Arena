using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpawnWaveJob
    {
        public async UniTask Run(OvermindAspect overmind)
        {
            int agentsCount = overmind.Config.WaveAgentsCount.GetRandom();

            for (int i = 0; i < agentsCount; i++)
            {
                await new SpawnSingleEnemyJob().Run(overmind);
                await Wait.Seconds(0.5f, overmind.Entity);
            }
        }
    }
}
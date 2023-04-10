using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunOvermindBehaviorJob
    {
        public async void Run(OvermindAspect overmind)
        {
            Data data = Query.Single<Data>();

            while (overmind.Exist())
            {
                await new SpawnWaveJob().Run(overmind, data.Overmind.WaveAgentsCount);
                await UniTask.Delay(data.Overmind.WavesDelay.ToSeconds());
                await UniTask.WaitWhile(() => overmind.State.HasEnoughAgents());
            }
        }
    }
}
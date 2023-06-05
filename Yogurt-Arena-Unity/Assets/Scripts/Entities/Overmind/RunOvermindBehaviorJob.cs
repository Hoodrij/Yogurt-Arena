using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunOvermindBehaviorJob
    {
        public async void Run()
        {
            OvermindAspect overmind = Query.Single<OvermindAspect>();
            overmind.Run(Update);

            
            async UniTask Update()
            {
                await new SpawnWaveJob().Run(overmind);
                await UniTask.Delay(overmind.Data.WavesDelay.GetRandom().ToSeconds());
                await UniTask.WaitWhile(HasEnoughAgents);
            }
            bool HasEnoughAgents()
            {
                int minimumAgents = overmind.Data.MinimumAgents;
                return overmind.State.Agents.Count > minimumAgents;
            }
        }
    }
}
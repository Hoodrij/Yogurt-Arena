using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunOvermindBehaviorJob
    {
        public async void Run()
        {
            OvermindAspect overmind = Query.Single<OvermindAspect>();
            overmind.Run(Update);
            return;


            async UniTask Update()
            {
                await new SpawnWaveJob().Run(overmind);
                await Wait.Seconds(overmind.Config.WavesDelay.GetRandom(), overmind.Entity);
                await Wait.While(HasEnoughAgents, overmind.Entity);
                await Wait.While(MaximumLimitReached, overmind.Entity);
            }
            bool HasEnoughAgents()
            {
                int minimumAgents = overmind.Config.MinimumAgentsInWorld;
                return overmind.State.CurrentAgents.Count > minimumAgents;
            }
            bool MaximumLimitReached()
            {
                return overmind.State.TotalSpawned >= overmind.Config.TotalAgentsToSpawn;
            }
        }
    }
}
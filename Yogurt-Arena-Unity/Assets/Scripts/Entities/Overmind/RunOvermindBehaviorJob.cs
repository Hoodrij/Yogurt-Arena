using UnityEngine;

namespace Yogurt.Arena
{
    public struct RunOvermindBehaviorJob
    {
        public async void Run()
        {
            OvermindAspect overmind = Query.Single<OvermindAspect>();
            overmind.Run(Update);

            
            async Awaitable Update()
            {
                await new SpawnWaveJob().Run(overmind);
                await Wait.Seconds(overmind.Config.WavesDelay.GetRandom());
                await Wait.While(HasEnoughAgents);
            }
            bool HasEnoughAgents()
            {
                int minimumAgents = overmind.Config.MinimumAgents;
                return overmind.State.Agents.Count > minimumAgents;
            }
        }
    }
}
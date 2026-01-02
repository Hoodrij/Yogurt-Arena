namespace Yogurt.Arena;

public struct RunOvermindBehaviorJob
{
    public void Run()
    {
        OvermindAspect overmind = Query.Single<OvermindAspect>();
        overmind.Run(Update);
        return;


        async UniTask Update()
        {
            await new SpawnWaveJob().Run(overmind);
            await Wait.Seconds(overmind.Config.WavesDelay.GetRandom(), overmind.Life());
            await Wait.While(HasEnoughAgents, overmind.Life());
            await Wait.While(MaximumLimitReached, overmind.Life());
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
namespace Yogurt.Arena
{
    public struct UpdatePlayerDestinationJob : IUpdateJob
    {
        public void Update()
        {
            BeaconAspect beaconAspect = Query.Single<BeaconAspect>();
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<PlayerTag>().Single();

            agentAspect.Body.Destination = beaconAspect.Body.Destination;
        }
    }
}
namespace Yogurt.Arena
{
    public struct UpdatePlayerDestinationJob : IUpdateJob
    {
        public void Update()
        {
            BeaconAspect beaconAspect = Query.Single<BeaconAspect>();
            PlayerAspect playerAspect = Query.Single<PlayerAspect>();

            playerAspect.Agent.Body.Destination = beaconAspect.Body.Destination;
        }
    }
}
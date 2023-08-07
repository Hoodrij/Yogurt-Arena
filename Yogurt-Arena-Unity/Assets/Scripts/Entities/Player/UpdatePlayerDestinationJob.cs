namespace Yogurt.Arena
{
    public struct UpdatePlayerDestinationJob
    {
        public void Run(PlayerAspect player)
        {
            player.Run(Update);
            return;


            void Update()
            {
                BeaconAspect beaconAspect = Query.Single<BeaconAspect>();

                player.Agent.Body.Destination = beaconAspect.Body.Destination;
            }
        }
    }
}
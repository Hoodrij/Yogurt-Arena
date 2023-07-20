using UnityEngine;

namespace Yogurt.Arena
{
    public struct RunWorldJob
    {
        public async Awaitable Run()
        {
            Entity world = await new WorldFactoryJob().Run();
            
            world.Run(new UpdateMoveInputJob());
            world.Run(new BeaconMoveJob());
            world.Run(new CameraFollowJob());
            world.Run(new UpdatePlayerDestinationJob());
            world.Run(new UpdateOvermindDestinationJob());
            world.Run(new AgentMoveJob());
            world.Run(new AgentLookJob());
            
            new StartScenarioJob().Run();
        }
    }
}
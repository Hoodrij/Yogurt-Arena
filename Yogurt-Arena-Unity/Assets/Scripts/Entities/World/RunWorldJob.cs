﻿using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunWorldJob
    {
        public async UniTask Run()
        {
            Entity world = await new WorldFactoryJob().Run();
            
            world.Run(new UpdateMoveInputJob());
            world.Run(new BeaconMoveJob());
            world.Run(new CameraFollowJob());
            world.Run(new AgentMoveJob());
            world.Run(new AgentLookJob());

            
        }
    }
}
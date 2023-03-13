﻿using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask<Entity> Run()
        {
            Entity entity = Entity.Create()
                .AddDisposable(await Assets.World.Spawn());

            await new LevelFactoryJob().Run();
            await new CameraFactoryJob().Run();
            await new InputFieldFactoryJob().Run();
            await new BeaconFactoryJob().Run();
            await new PlayerFactoryJob().Run();
            await new OvermindFactoryJob().Run();

            return entity;
        }
    }
}
﻿using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask<Entity> Run()
        {
            Entity entity = Entity.Create()
                .Add(await Assets.World.Spawn());

            await new CameraFactoryJob().Run();
            await new InputFieldFactoryJob().Run();

            return entity;
        }
    }
}
﻿using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        public async UniTask<CameraAspect> Run()
        {
            CameraConfig cameraConfig = new GetConfigJob().Run<CameraConfig>();
            CameraView view = await cameraConfig.Asset.Spawn();

            CameraAspect camera = World.Create()
                .Link(view.gameObject)
                .Add(cameraConfig)
                .Add(view)
                .As<CameraAspect>();

            new CameraFollowJob().Run(camera);

            return camera;
        }
    }
}
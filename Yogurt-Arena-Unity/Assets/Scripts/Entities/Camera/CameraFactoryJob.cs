using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        public async Awaitable<CameraAspect> Run()
        {
            CameraConfig cameraConfig = Query.Single<Config>().Camera;
            CameraView view = await cameraConfig.Asset.Spawn();

            CameraAspect camera = World.Create()
                .AddLink(view.gameObject)
                .Add(cameraConfig)
                .Add(view)
                .As<CameraAspect>();

            new CameraFollowJob().Run(camera);

            return camera;
        }
    }
}
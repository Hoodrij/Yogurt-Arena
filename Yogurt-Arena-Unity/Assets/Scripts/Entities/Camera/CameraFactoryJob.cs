using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        public async Awaitable Run()
        {
            CameraConfig cameraConfig = Query.Single<Config>().Camera;
            CameraView view = await cameraConfig.Asset.Spawn();

            Entity entity = World.Create()
                .AddLink(view.gameObject)
                .Add(cameraConfig)
                .Add(view);
        }
    }
}
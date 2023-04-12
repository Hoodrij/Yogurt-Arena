using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        public async UniTask Run()
        {
            CameraData cameraData = Query.Single<Data>().Camera;
            CameraView view = await cameraData.Asset.Spawn();

            Entity entity = World.Create()
                .AddLink(view.gameObject)
                .Add(cameraData)
                .Add(view);
        }
    }
}
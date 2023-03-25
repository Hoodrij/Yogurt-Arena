using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask Run()
        {
            CameraView cameraView = await Assets.Camera.Spawn();

            Entity entity = World.Create()
                .AddLink(cameraView.gameObject)
                .Add(cameraView);
        }
    }
}
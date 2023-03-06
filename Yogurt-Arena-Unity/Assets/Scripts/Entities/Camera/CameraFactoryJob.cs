using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask Run()
        {
            CameraView cameraView = await Assets.Camera.Spawn();

            Entity.Create()
                .Add(cameraView);
        }
    }
}
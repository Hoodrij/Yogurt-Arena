using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunWorldJob
    {
        public async UniTask Run()
        {
            await new WorldFactoryJob().Run();
            new UpdateMoveInputJob().Run();
            new UpdateCameraPositionJob().Run();
        }
    }
}
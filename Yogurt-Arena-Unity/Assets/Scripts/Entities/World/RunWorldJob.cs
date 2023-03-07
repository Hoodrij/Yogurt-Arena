using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunWorldJob
    {
        public async UniTask Run()
        {
            Entity entity = await new WorldFactoryJob().Run();


            entity.Run(new UpdateMoveInputJob());
            entity.Run(new UpdateCameraPositionJob());
        }
    }
}
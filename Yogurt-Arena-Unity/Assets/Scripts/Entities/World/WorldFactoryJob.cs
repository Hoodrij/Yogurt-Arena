using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask Run()
        {
            Entity.Create()
                .Add(new World
                {
                    GameObject = await Assets.World.Spawn()
                });

            await new CameraFactoryJob().Run();
            await new InputFieldFactoryJob().Run();
        }
    }
}
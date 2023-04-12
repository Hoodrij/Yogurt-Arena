using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            World world = await Query.Single<Data>().World.Spawn();
            
            Entity entity = Entity.Create()
                .AddLink(world.gameObject)
                .Add(world);

            await new LevelFactoryJob().Run();
            await new CameraFactoryJob().Run();
            await new InputFieldFactoryJob().Run();
            await new BeaconFactoryJob().Run();
            await new PlayerFactoryJob().Run();
            await new OvermindFactoryJob().Run();

            return entity;
        }
    }
}
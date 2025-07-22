using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            WorldConfig config = new GetConfigJob().Run<WorldConfig>();
            World world = await config.World.Spawn();
            
            Entity entity = Entity.Create()
                .Link(world.gameObject)
                .Add(world)
                .Add(new Level());

            await new LocationFactoryJob().Run();
            await new UIFactoryJob().Run();
            await new WorldUIFactoryJob().Run();
            await new InputFieldFactoryJob().Run();
            await new BeaconFactoryJob().Run();
            await new CameraFactoryJob().Run();
            await new PlayerFactoryJob().Run();
            await new OvermindFactoryJob().Run();
            await new ItemsSpawnerFactory().Run();

            return entity;
        }
    }
}
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            BeaconView beaconView = await Query.Single<Assets>().Beacon.Spawn();

            Entity entity = World.Create()
                .Add(beaconView)
                .Add<BeaconState>();

            return entity;
        }
    }
}
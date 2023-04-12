using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            BeaconData beaconData = Query.Single<Data>().Beacon;
            BeaconView view = await beaconData.Asset.Spawn();

            Entity entity = World.Create()
                .Add(beaconData)
                .Add(view)
                .Add<BeaconBodyState>();

            return entity;
        }
    }
}
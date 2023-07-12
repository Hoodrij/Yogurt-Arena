using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async Awaitable<Entity> Run()
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
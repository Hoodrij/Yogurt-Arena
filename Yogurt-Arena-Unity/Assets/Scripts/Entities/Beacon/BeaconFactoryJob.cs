using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async Awaitable<Entity> Run()
        {
            BeaconConfig beaconConfig = Query.Single<Config>().Beacon;
            BeaconView view = await beaconConfig.Asset.Spawn();

            Entity entity = World.Create()
                .Add(beaconConfig)
                .Add(view)
                .Add<BeaconBodyState>();

            return entity;
        }
    }
}
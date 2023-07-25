using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async Awaitable<BeaconAspect> Run()
        {
            BeaconConfig beaconConfig = Query.Single<Config>().Beacon;
            BeaconView view = await beaconConfig.Asset.Spawn();

            BeaconAspect beacon = World.Create()
                .Add(beaconConfig)
                .Add(view)
                .Add<BeaconBodyState>()
                .As<BeaconAspect>();
            
            new BeaconMoveJob().Run(beacon);

            return beacon;
        }
    }
}
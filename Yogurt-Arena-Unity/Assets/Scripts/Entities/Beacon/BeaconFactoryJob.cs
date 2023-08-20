using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async UniTask<BeaconAspect> Run()
        {
            BeaconConfig beaconConfig = Query.Single<Config>().Beacon;
            BeaconView view = await beaconConfig.Asset.Spawn();

            BeaconAspect beacon = World.Create()
                .Add(beaconConfig)
                .Add(view)
                .Add(new BeaconBodyState())
                .As<BeaconAspect>();
            
            new BeaconMoveJob().Run(beacon);

            return beacon;
        }
    }
}
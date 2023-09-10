using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct BeaconFactoryJob
    {
        public async UniTask<BeaconAspect> Run()
        {
            BeaconConfig beaconConfig = new GetConfigJob().Run<BeaconConfig>();
            BeaconView view = await beaconConfig.Asset.Spawn();

            BeaconAspect beacon = World.Create()
                .AddLink(view.gameObject)
                .Add(beaconConfig)
                .Add(view)
                .Add(new BeaconBodyState())
                .As<BeaconAspect>();
            
            new BeaconMoveJob().Run(beacon);

            return beacon;
        }
    }
}
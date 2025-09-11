namespace Yogurt.Arena;

public struct BeaconFactoryJob
{
    public async UniTask<BeaconAspect> Run()
    {
        BeaconConfig beaconConfig = new GetConfigJob().Run<BeaconConfig>();

        BeaconAspect beacon = World.Create()
            .Add(beaconConfig)
            .Add(new BeaconBodyState())
            .As<BeaconAspect>();

        new BeaconMoveJob().Run(beacon);

        return beacon;
    }
}
namespace Yogurt.Arena;

public struct BeaconAspect : IAspect
{
    public Entity Entity { get; set; }

    public BeaconConfig Config => this.Get<BeaconConfig>();

    public ref BeaconBodyState Body => ref this.Get<BeaconBodyState>();
}
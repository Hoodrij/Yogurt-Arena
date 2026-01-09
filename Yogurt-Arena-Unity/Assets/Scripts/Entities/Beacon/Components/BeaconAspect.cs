namespace Yogurt.Arena;

public record struct BeaconAspect(Entity Entity) : IAspect
{
    public BeaconConfig Config => this.Get<BeaconConfig>();

    public ref BeaconBodyState Body => ref this.Get<BeaconBodyState>();
}
namespace Yogurt.Arena;

public record struct BeaconBodyState : IComponent
{
    public Vector3 RawDestination;
    public Vector3 Destination;
    public Sequence Animation;
}
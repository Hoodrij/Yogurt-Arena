namespace Yogurt.Arena;

[System.Serializable]
public record struct TargetDetectionConfig : IComponent
{
    public float Distance;
    public LayerMask CollisionMask;
    public float YTolerance;
}
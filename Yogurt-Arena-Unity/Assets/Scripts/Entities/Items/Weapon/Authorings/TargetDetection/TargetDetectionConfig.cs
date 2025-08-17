namespace Yogurt.Arena
{
    [System.Serializable]
    public class TargetDetectionConfig : IComponent
    {
        public float Distance;
        public LayerMask CollisionMask;
        public float YTolerance;
    }
}
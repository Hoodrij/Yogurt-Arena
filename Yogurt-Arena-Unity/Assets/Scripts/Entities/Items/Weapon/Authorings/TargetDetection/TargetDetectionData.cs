using UnityEngine;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class TargetDetectionData : IComponent
    {
        public float Distance;
        public LayerMask CollisionMask;
        public float YTolerance;
    }
}
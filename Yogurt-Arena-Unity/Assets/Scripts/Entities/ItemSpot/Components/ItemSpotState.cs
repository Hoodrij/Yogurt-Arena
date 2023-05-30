using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotState : IComponent
    {
        public EItemType Type;
        public float Radius;
        public LayerMask Mask;
    }
}
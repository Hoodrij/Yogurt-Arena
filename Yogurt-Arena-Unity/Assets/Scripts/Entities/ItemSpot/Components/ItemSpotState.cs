using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotState : IComponent
    {
        public ItemType Type;
        public float Radius;
        public LayerMask Mask;
    }
}
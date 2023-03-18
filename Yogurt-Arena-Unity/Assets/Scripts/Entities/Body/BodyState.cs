using UnityEngine;

namespace Yogurt.Arena
{
    public class BodyState : IComponent
    {
        public Vector3 Destination;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 LookTarget;
    }
}
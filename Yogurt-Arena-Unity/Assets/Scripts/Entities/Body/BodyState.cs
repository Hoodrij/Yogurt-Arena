namespace Yogurt.Arena
{
    public class BodyState : IComponent
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Destination;
        public Vector3 LookPoint;
        public Vector3 Forward = Vector3.back;
        public Vector3 MiddlePoint => Position.AddY(0.5f);
    }
}
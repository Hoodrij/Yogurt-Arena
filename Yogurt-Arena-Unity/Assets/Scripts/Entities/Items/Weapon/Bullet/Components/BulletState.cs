using UnityEngine;

namespace Yogurt.Arena
{
    public class BulletState : IComponent
    {
        public Entity Owner;
        public Rigidbody RigidBody;
    }
}
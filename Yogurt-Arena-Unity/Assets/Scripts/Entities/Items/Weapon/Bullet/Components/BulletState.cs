using UnityEngine;

namespace Yogurt.Arena
{
    public class BulletState : IComponent
    {
        public AgentAspect Owner;
        public Rigidbody RigidBody;
        public SphereCollider Collider;
    }
}
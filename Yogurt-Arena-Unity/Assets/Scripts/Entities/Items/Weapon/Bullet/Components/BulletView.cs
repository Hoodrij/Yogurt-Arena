using UnityEngine;

namespace Yogurt.Arena
{
    public class BulletView : MonoBehaviour, IComponent
    {
        public Rigidbody Body;
        public SphereCollider Collider;
        public TrailRenderer Trail;
    }
}
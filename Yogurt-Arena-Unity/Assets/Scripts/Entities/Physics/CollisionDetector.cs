using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class CollisionDetector : MonoBehaviour, IComponent
    {
        public Event<RaycastHit> CollisionEvent = new Event<RaycastHit>();
        
        public LayerMask Mask;

        private Rigidbody body;
        private SphereCollider collider;
        private float radius;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            collider = GetComponent<SphereCollider>();
            radius = collider.radius;
        }

        private void FixedUpdate()
        {
            Vector3 moveDir = body.velocity.normalized;
            float moveSpeed = body.velocity.magnitude * Time.fixedDeltaTime;
            
            if (Physics.SphereCast(transform.position, radius, moveDir, out RaycastHit hit, moveSpeed, Mask))
            {
                CollisionEvent.Fire(hit);
            }
        }
    }
}
using UnityEngine;

namespace Yogurt.Arena
{
    public struct BulletAspect : IAspect
    {
        public Entity Entity { get; set; }
        
        public BulletData Data => this.Get<BulletData>();

        public BulletView View => this.Get<BulletView>();
        public BulletState State => this.Get<BulletState>();

        
        public Vector3 Position => View.transform.position;

        public readonly (Vector3 moveDir, float moveSpeed) GetMoveData()
        {
            Rigidbody body = State.RigidBody;
            
            Vector3 moveDir = body.velocity.normalized;
            float moveSpeed = body.velocity.magnitude * Time.fixedDeltaTime;
            return (moveDir, moveSpeed);
        } 
        
        public readonly Vector3 GetNextPosition()
        {
            (Vector3 moveDir, float moveSpeed) = GetMoveData();
            Vector3 currentPos = Position;
            Vector3 nextPos = currentPos + moveDir * moveSpeed;
            return nextPos;
        } 
    }
}
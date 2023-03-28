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
    }
}
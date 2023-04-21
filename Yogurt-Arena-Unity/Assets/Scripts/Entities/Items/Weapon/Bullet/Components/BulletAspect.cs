namespace Yogurt.Arena
{
    public struct BulletAspect : IAspect
    {
        public Entity Entity { get; set; }
        
        public BulletData Data => this.Get<BulletData>();

        public BodyState Body => this.Get<BodyState>();
        public BulletState State => this.Get<BulletState>();
        public BulletView View => this.Get<BulletView>();
    }
}
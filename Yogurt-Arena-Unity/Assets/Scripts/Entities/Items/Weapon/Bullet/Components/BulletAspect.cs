namespace Yogurt.Arena
{
    public struct BulletAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BulletView View => this.Get<BulletView>();
        public BulletState State => this.Get<BulletState>();
        public BodyState Body => this.Get<BodyState>();
    }
}
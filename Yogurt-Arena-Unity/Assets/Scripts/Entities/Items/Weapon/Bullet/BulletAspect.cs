namespace Yogurt.Arena
{
    public struct BulletAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BulletView View => this.Get<BulletView>();
        public BodyState Body => this.Get<BodyState>();
    }
}
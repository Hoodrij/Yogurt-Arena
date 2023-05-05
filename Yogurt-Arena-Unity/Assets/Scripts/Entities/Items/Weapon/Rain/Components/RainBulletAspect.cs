namespace Yogurt.Arena
{
    public struct RainBulletAspect : IAspect
    {
        public Entity Entity { get; set; }

        public RainBulletData Data => this.Get<RainBulletData>();
        
        public BulletAspect BulletAspect => this.Get<BulletAspect>();
        public BattleState BattleState => this.Get<BattleState>();
        public OwnerState Owner => this.Get<OwnerState>();
    }
}
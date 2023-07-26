namespace Yogurt.Arena
{
    public struct WeaponWithClipAspect : IAspect
    {
        public Entity Entity { get; set; }

        public WeaponClipConfig Config => this.Get<WeaponClipConfig>();
        public WeaponClipState State => this.Get<WeaponClipState>();
    }
}
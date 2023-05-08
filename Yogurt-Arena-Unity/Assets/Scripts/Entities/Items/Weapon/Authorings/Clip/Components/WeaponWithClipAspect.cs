namespace Yogurt.Arena
{
    public struct WeaponWithClipAspect : IAspect
    {
        public Entity Entity { get; set; }

        public WeaponClipData Data => this.Get<WeaponClipData>();
        public WeaponClipState State => this.Get<WeaponClipState>();
    }
}
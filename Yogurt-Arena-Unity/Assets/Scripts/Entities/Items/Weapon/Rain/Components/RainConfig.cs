using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class RainConfig : IComponent
    {
        public WeaponConfig Common;
        public ItemLifetimeConfig Lifetime;
        public WeaponClipData Clip;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
        public RainBulletData Bullet;
    }
}
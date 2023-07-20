using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class RainData : IComponent
    {
        public WeaponData Common;
        public ItemLifetimeData Lifetime;
        public WeaponClipData Clip;
        public WeaponScatteringData Scattering;
        public RainBulletData Bullet;
    }
}
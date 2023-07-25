using System;

namespace Yogurt.Arena.Components
{
    [Serializable]
    public class RifleConfig : IComponent
    {
        public WeaponConfig Common;
        public ItemLifetimeConfig Lifetime;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
    }
}
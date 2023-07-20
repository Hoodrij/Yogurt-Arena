using System;

namespace Yogurt.Arena.Components
{
    [Serializable]
    public class RifleData : IComponent
    {
        public WeaponData Common;
        public ItemLifetimeData Lifetime;
        public WeaponScatteringData Scattering;
    }
}
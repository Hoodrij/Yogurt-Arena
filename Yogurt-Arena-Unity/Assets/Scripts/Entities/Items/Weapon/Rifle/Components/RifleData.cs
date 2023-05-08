using System;

namespace Yogurt.Arena.Components
{
    [Serializable]
    public class RifleData : IComponent
    {
        public WeaponData CommonData;
        public WeaponScatteringData ScatteringData;
    }
}
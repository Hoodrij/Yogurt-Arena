using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class WeaponScatteringConfig : IComponent
    {
        public MinMax VelocityMagnitudeModifier = new MinMax(1, 1);
        public float Angle;
    }
}
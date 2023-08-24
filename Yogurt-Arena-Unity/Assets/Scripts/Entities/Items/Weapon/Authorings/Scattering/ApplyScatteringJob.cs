using UnityEngine;

namespace Yogurt.Arena
{
    public struct ApplyScatteringJob
    {
        public Vector3 Run(ItemAspect item, Vector3 initialVelocity)
        {
            WeaponScatteringConfig config = item.Get<WeaponScatteringConfig>();
            
            float scatteringAngle = (config.Angle / 2).RandomTo().WithRandomSign();
            Vector3 scatteredVelocity = Quaternion.AngleAxis(scatteringAngle, Vector3.up) * initialVelocity;
            scatteredVelocity *= config.VelocityMagnitudeModifier.GetRandom();
            return scatteredVelocity;
        }
    }
}
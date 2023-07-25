using UnityEngine;

namespace Yogurt.Arena
{
    public struct ApplyScatteringJob
    {
        public Vector3 Run(ItemAspect item, Vector3 initialDir)
        {
            WeaponScatteringConfig scatteringConfig = item.Get<WeaponScatteringConfig>();
            
            var scattering = (scatteringConfig.angle / 2).RandomTo().WithRandomSign();
            return Quaternion.AngleAxis(scattering, Vector3.up) * initialDir;
        }
    }
}
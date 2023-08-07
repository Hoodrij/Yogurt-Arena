using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class RifleConfig : ScriptableObject, IComponent, IConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new RifleFactoryJob(),
            UseJob = new UseRifleJob()
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this)
                .Add(Item)
                .Add(Weapon)
                .Add(Lifetime)
                .Add(Scattering)
                .Add(TargetDetection);
        }
    }
}
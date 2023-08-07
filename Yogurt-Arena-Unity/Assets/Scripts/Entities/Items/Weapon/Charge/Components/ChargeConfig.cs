using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class ChargeConfig : ScriptableObject, IComponent, IConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new ChargeFactoryJob(),
            UseJob = new UseChargeJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this)
                .Add(Item)
                .Add(Weapon)
                .Add(Lifetime)
                .Add(TargetDetection);
        }
    }
}
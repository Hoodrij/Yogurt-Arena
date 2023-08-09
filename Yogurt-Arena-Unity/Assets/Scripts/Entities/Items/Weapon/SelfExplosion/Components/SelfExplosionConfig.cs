using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class SelfExplosionConfig : ScriptableObject, IComponent, IConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new SelfExplosionFactory(),
            UseJob = new UseSelfExplosionJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;
        public ExplosionConfig Explosion;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this)
                .Add(Item)
                .Add(Weapon)
                .Add(Lifetime)
                .Add(TargetDetection)
                .Add(Explosion)
                ;
        }
    }
}
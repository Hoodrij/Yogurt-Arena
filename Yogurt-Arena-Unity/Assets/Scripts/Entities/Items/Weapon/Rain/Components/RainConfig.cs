using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class RainConfig : ScriptableObject, IComponent, IConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new RainFactoryJob(),
            UseJob = new UseRainJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public WeaponClipConfig Clip;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
        public RainBulletConfig Bullet;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this)
                .Add(Item)
                .Add(Weapon)
                .Add(Lifetime)
                .Add(Clip)
                .Add(Scattering)
                .Add(TargetDetection)
                .Add(Bullet);
        }
    }
}
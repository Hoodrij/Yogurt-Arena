namespace Yogurt.Arena
{
    public class RainConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseRainJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public WeaponClipConfig Clip;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
        public RainBulletConfig Bullet;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
            entity.Add(Item);
            entity.Add(Weapon);
            entity.Add(Lifetime);
            entity.Add(Clip);
            entity.Add(Scattering);
            entity.Add(TargetDetection);
            entity.Add(Bullet);
        }
    }
}
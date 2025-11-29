namespace Yogurt.Arena
{
    public class BoomerWeaponConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseBoomerWeaponJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;
        public ExplosionConfig Explosion;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
            entity.Add(Item);
            entity.Add(Weapon);
            entity.Add(Lifetime);
            entity.Add(TargetDetection);
            entity.Add(Explosion);
        }
    }
}
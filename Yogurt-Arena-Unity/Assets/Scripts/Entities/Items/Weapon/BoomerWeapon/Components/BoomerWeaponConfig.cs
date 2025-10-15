namespace Yogurt.Arena
{
    public class BoomerWeaponConfig : ScriptableObject, IComponent, IConfigSO
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
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return TargetDetection;
            yield return Explosion;
        }
    }
}
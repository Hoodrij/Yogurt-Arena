namespace Yogurt.Arena
{
    public class RainConfig : ScriptableObject, IComponent, IConfigSO
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
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return Clip;
            yield return Scattering;
            yield return TargetDetection;
            yield return Bullet;
        }
    }
}
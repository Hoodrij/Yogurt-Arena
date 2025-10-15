namespace Yogurt.Arena
{
    public class ChargerWeaponConfig : ScriptableObject, IComponent, IConfigSO
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseChargerWeaponJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return TargetDetection;
        }
    }
}
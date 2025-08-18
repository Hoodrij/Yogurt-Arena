namespace Yogurt.Arena
{
    public class RifleConfig : ScriptableObject, IComponent, IConfigSO
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseRifleJob(),
        };

        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;
        public WeaponClipConfig Clip;
        public int BulletsInShot;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return Scattering;
            yield return TargetDetection;
            yield return Clip;
        }
    }
}
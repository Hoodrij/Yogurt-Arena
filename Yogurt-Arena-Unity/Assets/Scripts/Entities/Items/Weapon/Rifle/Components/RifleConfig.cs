namespace Yogurt.Arena
{
    public class RifleConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
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
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
            entity.Add(Item);
            entity.Add(Weapon);
            entity.Add(Lifetime);
            entity.Add(Scattering);
            entity.Add(TargetDetection);
            entity.Add(Clip);
        }
    }
}
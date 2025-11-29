namespace Yogurt.Arena
{
    public class ChargerWeaponConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseChargerWeaponJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
            entity.Add(Item);
            entity.Add(Weapon);
            entity.Add(Lifetime);
            entity.Add(TargetDetection);
        }
    }
}
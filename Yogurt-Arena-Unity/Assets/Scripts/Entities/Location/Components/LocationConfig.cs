namespace Yogurt.Arena
{
    public class LocationConfig : ScriptableObject, IConfigSO, IComponent, ILeveledConfig, IBlueprint
    {
        [field: SerializeField]
        public int Level { get; set; }

        public Asset<LocationPartTag> Asset;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
namespace Yogurt.Arena
{
    public class ItemSpawnerConfig : ScriptableObject, IConfigSO, ILeveledConfig, IComponent, IBlueprint
    {
        [field: SerializeField]
        public int Level { get; set; }

        public ItemType AvailableItems;
        public ItemTags AvailableTags;
        public int ItemsCount;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
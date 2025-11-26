namespace Yogurt.Arena
{
    public class ItemSpawnerConfig : ScriptableObject, IConfigSO, ILeveledConfig, IComponent
    {
        [field: SerializeField]
        public int Level { get; set; }

        public ItemType AvailableItems;
        public ItemTags AvailableTags;
        public int ItemsCount;
    }
}
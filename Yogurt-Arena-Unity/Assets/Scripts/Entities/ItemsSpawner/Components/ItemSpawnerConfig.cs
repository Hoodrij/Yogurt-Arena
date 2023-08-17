using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class ItemSpawnerConfig : ScriptableObject, IConfig, ILeveledConfig, IComponent
    {
        [field: SerializeField]
        public int Level { get; set; }

        public ItemType ForceItem;
        public ItemTags ForceTags;
        public int ItemsCount;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class ItemSpawnerConfig : ScriptableObject, IEntityConfig, ILeveledConfig, IComponent
    {
        [field: SerializeField]
        public int Level { get; set; }

        public ItemType ForceItem;
        public ItemTags ForceTags;
        public int ItemsCount;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
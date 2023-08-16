using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class LocationConfig : ScriptableObject, IConfig, IComponent, ILeveledConfig
    {
        [field: SerializeField]
        public int Level { get; set; }
        
        public Asset<LocationPartTag> Asset;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this);
        }
    }
}
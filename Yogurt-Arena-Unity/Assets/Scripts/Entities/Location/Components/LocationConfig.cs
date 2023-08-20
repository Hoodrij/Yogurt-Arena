using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class LocationConfig : ScriptableObject, IEntityConfig, IComponent, ILeveledConfig
    {
        [field: SerializeField]
        public int Level { get; set; }
        
        public Asset<LocationPartTag> Asset;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public class LocationConfig : ScriptableObject, IConfigSO, IComponent, ILeveledConfig
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
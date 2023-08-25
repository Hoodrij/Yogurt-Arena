using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]    
    public class OvermindConfig : ScriptableObject, IComponent, ILeveledConfig, IEntityConfig
    {
        [field: SerializeField]
        public int Level { get; set; }

        public AgentType AvailableTypes;
        public MinMaxInt WaveAgentsCount;
        public int TotalAgentsToSpawn;
        public int MinimumAgentsInWorld;
        public MinMax WavesDelay;
        public float MinRangeToPlayer;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
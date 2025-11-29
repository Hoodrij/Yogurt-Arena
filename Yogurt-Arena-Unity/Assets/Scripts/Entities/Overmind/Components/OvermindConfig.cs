namespace Yogurt.Arena
{
    public class OvermindConfig : ScriptableObject, IComponent, ILeveledConfig, IConfigSO, IBlueprint
    {
        [field: SerializeField]
        public int Level { get; set; }

        public AgentType AvailableTypes;
        public MinMaxInt WaveAgentsCount;
        public int TotalAgentsToSpawn;
        public int MinimumAgentsInWorld;
        public MinMax WavesDelay;
        public float MinRangeToPlayer;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
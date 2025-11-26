namespace Yogurt.Arena
{
    public class LocationConfig : ScriptableObject, IConfigSO, IComponent, ILeveledConfig
    {
        [field: SerializeField]
        public int Level { get; set; }

        public Asset<LocationPartTag> Asset;
    }
}
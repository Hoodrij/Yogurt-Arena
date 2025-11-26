namespace Yogurt.Arena
{
    public class WorldConfig : ScriptableObject, IComponent, IConfigSO
    {
        public Asset<World> World;
    }
}
namespace Yogurt.Arena
{
    public class WorldConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public Asset<World> World;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
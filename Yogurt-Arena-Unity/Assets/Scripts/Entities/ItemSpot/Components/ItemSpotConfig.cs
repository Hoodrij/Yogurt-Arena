namespace Yogurt.Arena
{
    public class ItemSpotConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public int Radius;
        public LayerMask Mask;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
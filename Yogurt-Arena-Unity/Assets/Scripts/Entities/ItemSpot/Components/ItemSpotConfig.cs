namespace Yogurt.Arena
{
    public class ItemSpotConfig : ScriptableObject, IComponent, IConfigSO
    {
        public int Radius;
        public LayerMask Mask;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
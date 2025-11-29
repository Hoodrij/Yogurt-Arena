namespace Yogurt.Arena
{
    public class UIConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public Asset<UIView> UI;
        public Asset<WorldUIView> WorldUI;
        public PooledAsset<HealthWidget> WorldHealthWidget;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}
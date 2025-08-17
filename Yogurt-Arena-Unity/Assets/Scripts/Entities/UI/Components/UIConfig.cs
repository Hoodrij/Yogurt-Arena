namespace Yogurt.Arena;

public class UIConfig : ScriptableObject, IComponent, IConfigSO
{
    public Asset<UIView> UI;
    public Asset<WorldUIView> WorldUI;
    public PooledAsset<HealthWidget> WorldHealthWidget;
        
    public IEnumerable<IComponent> GetComponents()
    {
        yield return this;
    }
}
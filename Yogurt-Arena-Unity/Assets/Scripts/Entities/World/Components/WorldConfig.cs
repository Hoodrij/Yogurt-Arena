namespace Yogurt.Arena;

public class WorldConfig : ScriptableObject, IComponent, IConfigSO
{
    public Asset<World> World;

    public IEnumerable<IComponent> GetComponents()
    {
        yield return this;
    }
}
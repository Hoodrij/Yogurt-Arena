namespace Yogurt.Arena;

public class EntityBlueprint : IComponent
{
    public IBlueprint Blueprint;
}

public interface IBlueprint
{
    void Populate(Entity entity);
}
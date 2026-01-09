namespace Yogurt.Arena;

public record struct EntityBlueprint : IComponent
{
    public IBlueprint Blueprint;
}

public interface IBlueprint
{
    void Populate(Entity entity);
}
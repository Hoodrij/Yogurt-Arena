namespace Yogurt.Arena;

public interface ISharedComponent<T> : IComponent where T : IComponent
{
    
}
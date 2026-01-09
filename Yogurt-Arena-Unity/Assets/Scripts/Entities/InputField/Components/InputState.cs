namespace Yogurt.Arena;

public record struct InputState : IComponent
{
    public bool HasClick;
    public Vector3 ClickWorldPosition;
}
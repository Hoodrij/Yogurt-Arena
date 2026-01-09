namespace Yogurt.Arena;

public record struct OwnerState : IComponent
{
    public AgentAspect Value;
        
    public static implicit operator AgentAspect(OwnerState ownerState) => ownerState.Value;
}
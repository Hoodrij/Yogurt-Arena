namespace Yogurt.Arena;

public class OwnerState : IComponent
{
    public AgentAspect Value;
        
    public static implicit operator AgentAspect(OwnerState ownerState) => ownerState.Value;
}
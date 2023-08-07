namespace Yogurt.Arena
{
    public class OwnerState : IComponent
    {
        public AgentAspect Owner;
        
        public static implicit operator AgentAspect(OwnerState ownerState) => ownerState.Owner;
    }
}
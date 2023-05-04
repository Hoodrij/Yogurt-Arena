namespace Yogurt.Arena
{
    public class OwnerState : IComponent
    {
        public Entity Owner;
        
        public static implicit operator Entity(OwnerState ownerState) => ownerState.Owner;
    }
}
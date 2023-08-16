using Unity.AI.Navigation;

namespace Yogurt.Arena
{
    public struct LocationAspect : IAspect
    {
        public Entity Entity { get; set; }

        public Location Location => this.Get<Location>();

        public NavMeshSurface NavSurface => Location.NavSurface;
    }
}
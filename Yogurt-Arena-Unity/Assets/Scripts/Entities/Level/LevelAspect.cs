using Unity.AI.Navigation;

namespace Yogurt.Arena
{
    public struct LevelAspect : IAspect
    {
        public Entity Entity { get; set; }

        public Level Level => this.Get<Level>();

        public NavMeshSurface NavSurface => Level.NavSurface;
    }
}
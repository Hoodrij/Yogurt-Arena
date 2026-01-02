namespace Yogurt.Arena
{
    public class World : MonoBehaviour, IComponent
    {
        public static Entity Create()
        {
            Entity world = Query.Of<World>().Single();
            return Entity.Create()
                .SetParent(world);
        }
    }
}
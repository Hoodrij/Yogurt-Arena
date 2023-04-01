using UnityEngine;

namespace Yogurt.Arena
{
    public class World : MonoBehaviour, IComponent
    {
        public static Entity Create()
        {
            Entity World = Query.Of<World>().Single();
            return Entity.Create()
                .SetParent(World);
        }
    }
}
using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class World : MonoBehaviour, IComponent, IDisposable
    {
        public static Entity Create()
        {
            Entity World = Query.Of<World>().Single();
            return Entity.Create()
                .SetParent(World);
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
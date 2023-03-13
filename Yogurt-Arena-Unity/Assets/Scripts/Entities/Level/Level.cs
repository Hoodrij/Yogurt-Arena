using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Yogurt.Arena
{
    public class Level : MonoBehaviour, IComponent, IDisposable
    {
        public NavMeshSurface NavSurface;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
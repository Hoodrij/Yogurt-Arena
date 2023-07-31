using Unity.AI.Navigation;
using UnityEngine;

namespace Yogurt.Arena
{
    public class Level : MonoBehaviour, IComponent
    {
        public NavMeshSurface NavSurface;
        public int CurrentPart;
    }
}
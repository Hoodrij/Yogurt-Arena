using Unity.AI.Navigation;
using UnityEngine;

namespace Yogurt.Arena
{
    public class Location : MonoBehaviour, IComponent
    {
        public NavMeshSurface NavSurface;
        public int CurrentPart;
    }
}
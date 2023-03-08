using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public class AgentState : IComponent
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public NavMeshPath FullPath;
    }
}
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentAspect : IAspect
    {
        public Entity Entity { get; set; }

        public AgentView View => this.Get<AgentView>();
        public AgentState State => this.Get<AgentState>();

        public Transform Transform => View.transform;
    }
}
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentLookJob : IUpdateJob
    {
        public void Update()
        {
            foreach (AgentAspect agent in Query.Of<AgentAspect>())
            {
                if (agent.State.Velocity.magnitude < 0.01f) continue;

                agent.Transform.LookAt(agent.Transform.position + agent.State.Velocity.WithY(0));
            }
        }
    }
}
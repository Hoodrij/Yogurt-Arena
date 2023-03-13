using DG.Tweening;

namespace Yogurt.Arena
{
    public struct AgentLookJob : IUpdateJob
    {
        public void Update()
        {
            foreach (AgentAspect agent in Query.Of<AgentAspect>())
            {
                if (agent.State.Velocity.magnitude < 0.01f) continue;

                agent.Transform.DOLookAt(agent.Transform.position + agent.State.Velocity.WithY(0), 0.3f);
            }
        }
    }
}
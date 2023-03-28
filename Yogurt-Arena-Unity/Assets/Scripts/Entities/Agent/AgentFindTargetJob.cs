using System.Linq;

namespace Yogurt.Arena
{
    public class AgentFindTargetJob : IUpdateJob
    {
        public void Update()
        {
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>())
            {
                agentAspect.BattleState.Target = GetTarget(agentAspect);
            }
        }

        private Entity GetTarget(AgentAspect agent)
        {
            AgentAspect target = Query.Of<AgentAspect>()
                .Where(other => IsHostile(agent, other))
                .Where(other => IsInRange(agent, other))
                .OrderBy(other => GetDistance(agent, other))
                .FirstOrDefault();
            return target.Entity;
        }

        private float GetDistance(AgentAspect agent, AgentAspect other)
        {
            return (agent.Body.Position - other.Body.Position).magnitude.Abs();
        }

        private bool IsHostile(AgentAspect agent, AgentAspect other)
        {
            return !other.Id.Team.HasFlag(agent.Id.Team);
        }
        
        private bool IsInRange(AgentAspect agent, AgentAspect other)
        {
            return GetDistance(agent, other) < Query.Single<Data>().Agent.FindTargetDistance;
        }
    }
}
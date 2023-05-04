using System.Linq;

namespace Yogurt.Arena
{
    public struct AgentFindTargetJob : IUpdateJob
    {
        public void Update()
        {
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>())
            {
                agentAspect.BattleState.Target = GetTarget(agentAspect);
            }
            
            
            AgentAspect GetTarget(AgentAspect agent)
            {
                AgentAspect target = Query.Of<AgentAspect>()
                    .Where(other => IsHostile(agent, other))
                    .Where(other => IsInRange(agent, other))
                    .OrderBy(other => GetDistance(agent, other))
                    .FirstOrDefault();
                return target;
            }
            float GetDistance(AgentAspect agent, AgentAspect other)
            {
                return (agent.Body.Position - other.Body.Position).magnitude.Abs();
            }
            bool IsHostile(AgentAspect agent, AgentAspect other)
            {
                return !other.Id.Team.HasFlag(agent.Id.Team);
            }
            bool IsInRange(AgentAspect agent, AgentAspect other)
            {
                return GetDistance(agent, other) < agent.Data.FindTargetDistance;
            }
        }

    }
}
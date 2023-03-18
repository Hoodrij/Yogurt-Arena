using System.Collections.Generic;
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
            AgentAspect target = GetHostileAgents(agent).First();
            return target.Entity;
        }

        private IEnumerable<AgentAspect> GetHostileAgents(AgentAspect agent)
        {
            return Query.Of<AgentAspect>().Where(other =>
            {
                return !other.Id.Team.HasFlag(agent.Id.Team);
            });
        }
    }
}
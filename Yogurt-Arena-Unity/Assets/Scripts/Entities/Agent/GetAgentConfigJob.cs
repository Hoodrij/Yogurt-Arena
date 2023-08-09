using System.Linq;

namespace Yogurt.Arena
{
    public struct GetAgentConfigJob
    {
        public AgentConfig Run(TeamType requiredTeam)
        {
            Entity config = Query.Of<AgentConfig>()
                .Where(entity => entity.Get<AgentConfig>().Team == requiredTeam)
                .GetRandom();

            return config.Get<AgentConfig>();
        }
    }
}
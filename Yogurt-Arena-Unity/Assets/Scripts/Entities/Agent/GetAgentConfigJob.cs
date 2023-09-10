using System.Collections.Generic;
using System.Linq;

namespace Yogurt.Arena
{
    public struct GetAgentConfigJob
    {
        public AgentConfig Run(TeamType requiredTeam, AgentType availableTypes = AgentType.Any)
        {
            Entity result = Query.Of<AgentConfig>()
                .With<ConfigOfEntity>()
                .Where(FitsTeam)
                .Where(FitsType)
                .GetRandom();

            return result.Get<AgentConfig>();
            
            
            bool FitsTeam(Entity entity)
            {
                AgentConfig config = entity.Get<AgentConfig>();
                return requiredTeam.HasFlag(config.Team);
            }
            bool FitsType(Entity entity)
            {
                AgentConfig config = entity.Get<AgentConfig>();
                return availableTypes.HasFlag(config.Type);
            }
        }
    }
}
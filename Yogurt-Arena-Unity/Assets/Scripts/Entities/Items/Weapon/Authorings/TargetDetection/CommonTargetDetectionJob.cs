using System.Linq;

namespace Yogurt.Arena
{
    public struct CommonTargetDetectionJob
    {
        public void Run(ItemAspect weapon)
        {
            TargetDetectionData data = weapon.Get<TargetDetectionData>();
            BattleState battleState = weapon.Get<BattleState>();
            
            weapon.Run(Update);
            
            
            void Update()
            {
                battleState.Target = GetTarget(weapon.Owner);
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
            bool IsHostile(AgentAspect agent, AgentAspect other)
            {
                return !other.Id.Team.HasFlag(agent.Id.Team);
            }
            bool IsInRange(AgentAspect agent, AgentAspect other)
            {
                return GetDistance(agent, other) < data.Distance;
                return true;
            }
            float GetDistance(AgentAspect agent, AgentAspect other)
            {
                return (agent.Body.Position - other.Body.Position).magnitude.Abs();
            }
        }
    }
}
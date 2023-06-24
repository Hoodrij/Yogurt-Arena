using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UpdateRainTargetJob
    {
        public async UniTaskVoid Run(RainBulletAspect bullet)
        {
            RainBulletData rainData = bullet.Data;
            AgentAspect owner = bullet.Owner;

            while (bullet.Exist())
            {
                bullet.BattleState.Target = GetTarget();
                await Wait.Update();
            }
                
                
            AgentAspect GetTarget()
            {
                AgentAspect target = Query.Of<AgentAspect>()
                    .Where(IsHostile)
                    .Where(IsInRange)
                    .OrderBy(GetDistance)
                    .FirstOrDefault();
                return target;
            }
            bool IsHostile(AgentAspect other)
            {
                return !other.Id.Team.HasFlag(owner.Id.Team);
            }
            bool IsInRange(AgentAspect other)
            {
                return GetDistance(other) < rainData.FindTargetDistance;
            }
            float GetDistance(AgentAspect other)
            {
                return (bullet.BulletAspect.Body.Position - other.Body.Position).magnitude.Abs();
            }
        }
    }
}
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UpdateRainTargetJob
    {
        public async void Run(BulletAspect bullet)
        {
            RainData rainData = bullet.Get<RainData>();
            AgentAspect owner = bullet.Get<OwnerState>().Owner;
            BattleState battleState = bullet.Get<BattleState>();

            while (bullet.Exist())
            {
                battleState.Target = GetTarget();
                await UniTask.Yield();
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
                return (bullet.Body.Position - other.Body.Position).magnitude.Abs();
            }
        }
    }
}
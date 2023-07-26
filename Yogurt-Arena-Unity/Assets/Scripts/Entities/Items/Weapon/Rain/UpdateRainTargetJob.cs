using System.Linq;

namespace Yogurt.Arena
{
    public struct UpdateRainTargetJob
    {
        public async void Run(RainBulletAspect bullet)
        {
            RainBulletData rainData = bullet.Data;
            AgentAspect owner = bullet.Owner;

            bullet.Run(Update);


            void Update()
            {
                bullet.BattleState.Target = GetTarget();
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
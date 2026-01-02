using Cathei.LinqGen;

namespace Yogurt.Arena;

public struct UpdateRainTargetJob
{
    public void Run(RainBulletAspect bullet)
    {
        RainBulletConfig rainConfig = bullet.Config;
        AgentAspect owner = bullet.Owner;

        bullet.Run(Update);
        return;


        void Update()
        {
            bullet.BattleState.Target = GetTarget();
        }
        AgentAspect GetTarget()
        {
            AgentAspect target = Query.Of<AgentAspect>().AsEnumerable()
                .Gen()
                .Where(IsHostile)
                .Where(IsInRange)
                .OrderBy(GetDistance)
                .FirstOrDefault();
            return target;
        }
        bool IsHostile(AgentAspect other)
        {
            return !other.Id.teamType.HasFlagNonAlloc(owner.Id.teamType);
        }
        bool IsInRange(AgentAspect other)
        {
            return GetDistance(other) < rainConfig.FindTargetDistance;
        }
        float GetDistance(AgentAspect other)
        {
            return (bullet.BulletAspect.Body.Position - other.Body.Position).magnitude.Abs();
        }
    }
}
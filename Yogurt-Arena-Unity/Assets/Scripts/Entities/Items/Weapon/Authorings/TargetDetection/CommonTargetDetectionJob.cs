using System.Linq;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct CommonTargetDetectionJob
    {
        public void Run(ItemAspect weapon)
        {
            TargetDetectionData data = weapon.Get<TargetDetectionData>();
            BattleState battleState = weapon.Get<BattleState>();
            AgentAspect agent = weapon.Owner.Owner;
            
            weapon.Run(Update);
            
            
            void Update()
            {
                battleState.Target = GetTarget();
            }
            AgentAspect GetTarget()
            {
                AgentAspect target = Query.Of<AgentAspect>()
                    .Where(IsHostile)
                    .Where(IsInRange)
                    .Where(IsNotBlockedByEnv)
                    .Where(IsReachableByY)
                    .OrderBy(GetDistance)
                    .FirstOrDefault();
                return target;
            }
            
            bool IsHostile(AgentAspect target)
            {
                return !target.Id.Team.HasFlag(agent.Id.Team);
            }
            bool IsInRange(AgentAspect target)
            {
                return GetDistance(target) < data.Distance;
            }
            bool IsNotBlockedByEnv(AgentAspect target)
            {
                Vector3 firePoint = agent.Body.Position.AddY(0.5f);
                Vector3 targetBodyCenter = target.Body.Position.AddY(0.5f);
                Vector3 vectorToTarget = targetBodyCenter - firePoint;
                Ray ray = new Ray
                {
                    origin = firePoint,
                    direction = vectorToTarget
                };

                bool hasEnvHit = Physics.Raycast(ray, vectorToTarget.magnitude, data.CollisionMask);
                return !hasEnvHit;
            }
            bool IsReachableByY(AgentAspect target)
            {
                float firePointY = agent.Body.Position.y;
                float targetY = target.Body.Position.y;
                return (firePointY - targetY).Abs() <= data.YTolerance;
            }
            
            float GetDistance(AgentAspect target)
            {
                return (agent.Body.Position - target.Body.Position).magnitude.Abs();
            }
        }
    }
}
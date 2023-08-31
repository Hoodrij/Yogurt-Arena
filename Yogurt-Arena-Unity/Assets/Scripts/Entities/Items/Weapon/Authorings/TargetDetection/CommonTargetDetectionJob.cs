using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct CommonTargetDetectionJob
    {
        public async UniTaskVoid Run(ItemAspect weapon)
        {
            TargetDetectionConfig config = weapon.Get<TargetDetectionConfig>();
            BattleState battleState = weapon.Get<BattleState>();
            AgentAspect agent = weapon.Owner.Value;
            
            weapon.Run(Update);

            await weapon.Entity.WaitForDead();
            battleState.Target = default;

            return;


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
                return !target.Id.teamType.HasFlag(agent.Id.teamType);
            }
            bool IsInRange(AgentAspect target)
            {
                return GetDistance(target) < config.Distance;
            }
            bool IsNotBlockedByEnv(AgentAspect target)
            {
                Vector3 firePoint = agent.Body.MiddlePoint;
                Vector3 targetBodyCenter = target.Body.MiddlePoint;
                Vector3 vectorToTarget = targetBodyCenter - firePoint;
                Ray ray = new Ray
                {
                    origin = firePoint,
                    direction = vectorToTarget
                };

                bool hasEnvHit = Physics.Raycast(ray, vectorToTarget.magnitude, config.CollisionMask);
                return !hasEnvHit;
            }
            bool IsReachableByY(AgentAspect target)
            {
                float firePointY = agent.Body.Position.y;
                float targetY = target.Body.Position.y;
                return (firePointY - targetY).Abs() <= config.YTolerance;
            }
            
            float GetDistance(AgentAspect target)
            {
                return (agent.Body.Position - target.Body.Position).magnitude.Abs();
            }
        }
    }
}
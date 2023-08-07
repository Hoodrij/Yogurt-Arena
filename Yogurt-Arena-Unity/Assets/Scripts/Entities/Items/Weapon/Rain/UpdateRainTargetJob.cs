﻿using System.Linq;

namespace Yogurt.Arena
{
    public struct UpdateRainTargetJob
    {
        public async void Run(RainBulletAspect bullet)
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
                return GetDistance(other) < rainConfig.FindTargetDistance;
            }
            float GetDistance(AgentAspect other)
            {
                return (bullet.BulletAspect.Body.Position - other.Body.Position).magnitude.Abs();
            }
        }
    }
}
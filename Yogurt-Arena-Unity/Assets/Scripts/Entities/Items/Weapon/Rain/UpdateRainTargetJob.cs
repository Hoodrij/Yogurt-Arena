namespace Yogurt.Arena;

public struct UpdateRainTargetJob
{
    public void Run(RainBulletAspect bullet)
    {
        UpdateJob updateJob = new UpdateJob
        {
            Bullet = bullet
        };

        bullet.Run(updateJob.Update);
    }

    private struct UpdateJob
    {
        public RainBulletAspect Bullet;
        private ref BattleState BattleState => ref Bullet.BattleState;
        private ref AgentAspect Owner => ref Bullet.Owner.Value;
        private RainBulletConfig Config => Bullet.Config;

        public void Update()
        {
            BattleState.Target = GetTarget();
        }

        private AgentAspect GetTarget()
        {
            AgentAspect closestTarget = default;
            float closestDistance = float.MaxValue;

            foreach (AgentAspect target in Query.Of<AgentAspect>())
            {
                if (!IsHostile(target)) continue;
                if (!IsInRange(target)) continue;

                float distance = GetDistance(target);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }

        private bool IsHostile(AgentAspect target)
        {
            return !target.Id.TeamType.HasFlagNonAlloc(Owner.Id.TeamType);
        }

        private bool IsInRange(AgentAspect target)
        {
            return GetDistance(target) < Config.FindTargetDistance;
        }

        private float GetDistance(AgentAspect target)
        {
            return (Bullet.BulletAspect.Body.Position - target.Body.Position).magnitude.Abs();
        }
    }
}
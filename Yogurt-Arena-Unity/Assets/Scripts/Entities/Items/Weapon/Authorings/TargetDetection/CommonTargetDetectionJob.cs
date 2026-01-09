namespace Yogurt.Arena;

public struct CommonTargetDetectionJob
{
    public async UniTaskVoid Run(ItemAspect weapon)
    {
        // ref BattleState battleState = ref weapon.Get<BattleState>();

        UpdateJob updateJob = new UpdateJob
        {
            Weapon = weapon
        };

        weapon.Run(updateJob.Update);

        await weapon.Life();
        weapon.Get<BattleState>().Target = default;
    }

    private struct UpdateJob
    {
        public ItemAspect Weapon;
        private ref BattleState BattleState => ref Weapon.Get<BattleState>();
        private ref TargetDetectionConfig Config => ref Weapon.Get<TargetDetectionConfig>();
        private ref AgentAspect Agent => ref Weapon.Owner.Value;

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
                if (!IsNotBlockedByEnv(target)) continue;
                if (!IsReachableByY(target)) continue;

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
            return !target.Id.TeamType.HasFlagNonAlloc(Agent.Id.TeamType);
        }

        private bool IsInRange(AgentAspect target)
        {
            return GetDistance(target) < Config.Distance;
        }

        private bool IsNotBlockedByEnv(AgentAspect target)
        {
            Vector3 firePoint = Agent.Body.MiddlePoint;
            Vector3 targetBodyCenter = target.Body.MiddlePoint;
            Vector3 vectorToTarget = targetBodyCenter - firePoint;
            Ray ray = new Ray
            {
                origin = firePoint,
                direction = vectorToTarget
            };

            bool hasEnvHit = Physics.Raycast(ray, vectorToTarget.magnitude, Config.CollisionMask);
            return !hasEnvHit;
        }

        private bool IsReachableByY(AgentAspect target)
        {
            float firePointY = Agent.Body.Position.y;
            float targetY = target.Body.Position.y;
            return (firePointY - targetY).Abs() <= Config.YTolerance;
        }

        private float GetDistance(AgentAspect target)
        {
            return (Agent.Body.Position - target.Body.Position).magnitude.Abs();
        }
    }
}
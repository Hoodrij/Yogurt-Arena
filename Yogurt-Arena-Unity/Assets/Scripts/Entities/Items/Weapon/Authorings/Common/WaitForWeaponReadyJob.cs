using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForWeaponReadyJob
    {
        public async UniTask Run(ItemAspect item)
        {
            AgentAspect owner = item.Owner;
            WeaponConfig config = item.Get<WeaponConfig>();

            await Wait.Until(() => !owner.Has<Kinematic>(), owner.Life());
            await Wait.Until(() =>
                IsWeaponAlive() &&
                HasOwner() && 
                HasTarget() && 
                IsInRange() && 
                IsLookingAtTarget()
                , owner.Life());
            return;
            
            bool IsWeaponAlive() => item.Exist();
            bool HasOwner() => owner.Exist();
            bool HasTarget() => owner.BattleState.Target.Exist();
            bool IsLookingAtTarget()
            {
                AgentAspect target = owner.BattleState.Target;
                Vector3 lookDir = owner.Body.Forward;
                Vector3 vectorToTarget = target.Body.Position.WithY(0) - owner.Body.Position.WithY(0);
                Vector3 dirToTarget = vectorToTarget.normalized;
                
                float dot = Vector3.Dot(lookDir, dirToTarget);
                float lookAngle = dot.DotToAngle();
                bool isLookingAt = lookAngle < config.AngleToAttack;

                return isLookingAt;
            }
            bool IsInRange()
            {
                AgentAspect target = owner.BattleState.Target;
                float distanceToTarget = Mathf.Abs((target.Body.Position - owner.Body.Position).magnitude);
                bool isInRange = distanceToTarget < config.Range;

                return isInRange;
            }
        }
    }
}
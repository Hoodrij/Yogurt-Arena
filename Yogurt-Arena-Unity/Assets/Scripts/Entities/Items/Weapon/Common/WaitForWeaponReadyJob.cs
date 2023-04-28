using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForWeaponReadyJob
    {
        public async UniTask Run(ItemAspect item)
        {
            AgentAspect owner = item.Item.Owner;
            WeaponData data = item.Get<WeaponData>();

            await UniTask.WaitUntil(() => !owner.Has<Kinematic>());
            await UniTask.WaitUntil(() =>
                HasOwner() && 
                HasTarget() && 
                IsInRange() && 
                IsLookingAtTarget()
                );
            
            
            bool HasOwner()
            {
                return owner.Exist();
            }
            bool HasTarget()
            {
                return owner.BattleState.Target.Exist();
            }
            bool IsLookingAtTarget()
            {
                AgentAspect target = owner.BattleState.Target;
                Vector3 lookDir = owner.View.transform.forward;
                Vector3 vectorToTarget = target.View.transform.position.WithY(0) - owner.View.transform.position.WithY(0);
                Vector3 dirToTarget = vectorToTarget.normalized;
                
                float dot = Vector3.Dot(lookDir, dirToTarget);
                float lookAngle = dot.DotToAngle();
                bool isLookingAt = lookAngle < data.AngleToAttack;

                return isLookingAt;
            }
            bool IsInRange()
            {
                AgentAspect target = owner.BattleState.Target;
                float distanceToTarget = Mathf.Abs((target.View.transform.position - owner.View.transform.position).magnitude);
                bool isInRange = distanceToTarget < data.Range;

                return isInRange;
            }
        }
    }
}
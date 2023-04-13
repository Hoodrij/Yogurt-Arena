using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct UseChargeJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item, AgentAspect owner)
        {
            ChargeData data = item.Get<ChargeData>();

            while (item.Exist())
            {
                await WaitForActivation();
                await UniTask.WaitUntil(() => HasTarget() && IsInRange() && IsLookingAtTarget());

                owner.Add<Kinematic>();
                Transform transform = owner.View.transform;
                Vector3 attackPosition = transform.position + transform.forward * data.Strength;
                transform.DOMove(attackPosition, data.Duration);

                await UniTask.Delay(data.Duration.ToSeconds());
                NavMesh.SamplePosition(attackPosition, out var attackPositionHit, 100, NavMesh.AllAreas);
                owner.Body.Position = owner.Body.Destination = attackPositionHit.position;
                owner.Remove<Kinematic>();
                
                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
            
            async UniTask WaitForActivation()
            {
                await UniTask.WaitUntil(() => !owner.Has<Kinematic>());
            }
            
            bool HasTarget()
            {
                return owner.BattleState.Target.Exist();
            }

            bool IsLookingAtTarget()
            {
                AgentAspect target = owner.BattleState.Target;
                Vector3 lookDir = owner.View.transform.forward;
                Vector3 dirToTarget = (target.View.transform.position - owner.View.transform.position).normalized;
                
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
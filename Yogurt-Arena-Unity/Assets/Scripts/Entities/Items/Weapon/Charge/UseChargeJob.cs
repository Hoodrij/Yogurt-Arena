using Cysharp.Threading.Tasks;
using UnityEngine;

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
                await UniTask.WaitUntil(() => HasOwner() && HasTarget() && IsInRange() && IsLookingAtTarget());
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new FireBulletJob().Run(bullet, GetDir());
                await new ChargeBulletBehaviorJob().Run(bullet);

                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
            
            
            bool HasOwner()
            {
                return owner.Exist();
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
            Vector3 GetDir()
            {
                return owner.View.transform.forward;
            }
        }
    }
}
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeBehaviorJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            owner.Add<Kinematic>();
            // TryDealDamage();
            new ChargeUpdateBulletPositionJob().Run(bullet);

            Transform transform = owner.View.transform;
            float speed = bullet.Data.Speed;
            var tween = DOTween.To(() => speed, x => speed = x, 0, bullet.Data.LifeTime);
            tween.OnUpdate(() =>
            {
                Vector3 newPos = transform.position + transform.forward * speed;
                NavMesh.SamplePosition(newPos, out var hit, 10, NavMesh.AllAreas);

                transform.position = owner.Body.Position = owner.Body.Destination = hit.position;
            });

            await UniTask.WhenAny(WaitForOwnerDeath(), WaitForLifeTime());
            tween.Kill();
            if (owner.Exist())
            {
                owner.Remove<Kinematic>();
            }
            await new KillBulletJob().Run(bullet);

            
            async void TryDealDamage()
            {
                int damage = bullet.Data.Damage;
                CollisionInfo collisionInfo = await new WaitForBulletHitJob().Run(bullet);
                new DealDamageJob().Run(collisionInfo.Entity, damage);
            }
            async UniTask WaitForOwnerDeath() => await UniTask.WaitWhile(() => owner.Exist());
            async UniTask WaitForLifeTime() => await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}
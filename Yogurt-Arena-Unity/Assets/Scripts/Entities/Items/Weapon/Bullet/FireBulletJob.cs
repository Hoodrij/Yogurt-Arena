using UnityEngine;

namespace Yogurt.Arena
{
    public struct FireBulletJob
    {
        public void Run(BulletAspect bullet)
        {
            AgentBattleState ownerBattleState = bullet.State.Owner.Get<AgentBattleState>();
            BodyState targetBody = ownerBattleState.Target.Get<BodyState>();

            Vector3 dir = (targetBody.Position.WithY(0) - bullet.Body.Position.WithY(0))
                .WithY(0)
                .normalized;

            bullet.State.RigidBody.velocity = dir * 100;
        }
    }
}
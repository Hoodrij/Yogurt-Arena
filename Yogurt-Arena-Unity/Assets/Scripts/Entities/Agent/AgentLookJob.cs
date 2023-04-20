using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentLookJob : IUpdateJob
    {
        const float MIN_LOOK_MAGNITUDE = 0.001f;
        
        public void Update()
        {
            foreach (AgentAspect agent in Query.Of<AgentAspect>()
                         .Without<Kinematic>())
            {
                BodyState body = agent.Body;
                
                if (agent.BattleState.Target.Exist())
                {
                    BodyState targetBody = agent.BattleState.Target.Get<BodyState>();
                    body.LookPoint = targetBody.Position.WithY(body.Position.y);
                }
                else
                {
                    body.LookPoint = body.Position + body.Velocity.WithY(0);
                }

                Vector3 lookVector = body.LookPoint - body.Position;
                if (lookVector.sqrMagnitude < MIN_LOOK_MAGNITUDE)
                    continue;

                agent.View.transform.DOLookAt(body.LookPoint, agent.Data.LookSmoothValue);
            }
        }
    }
}
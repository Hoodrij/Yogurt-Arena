using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentLookJob
    {
        const float MIN_LOOK_MAGNITUDE = 0.001f;

        public void Run(AgentAspect agent)
        {
            agent.Run(Update);
            
            
            void Update()
            {
                if (agent.Has<Kinematic>())
                    return;
                
                int frameRate = Query.Single<Time>().TARGET_FRAME_RATE;

                BodyState body = agent.Body;
                
                if (agent.BattleState.Target.Exist())
                {
                    BodyState targetBody = agent.BattleState.Target.Get<BodyState>();
                    body.LookPoint = targetBody.Position.WithY(body.Position.y);
                }
                else
                {
                    body.LookPoint = body.Position + body.Velocity.WithY(0) * frameRate;
                }

                Vector3 lookVector = body.LookPoint - body.Position;
                if (lookVector.sqrMagnitude > MIN_LOOK_MAGNITUDE)
                {
                    agent.View.transform.DOLookAt(body.LookPoint, agent.Config.LookSmoothValue);
                }
            }
        }
        
    }
}
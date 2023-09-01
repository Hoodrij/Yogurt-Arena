using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentLookJob
    {
        const float MIN_LOOK_MAGNITUDE = 0.001f;

        public void Run(AgentAspect agent)
        {
            Time time = Query.Single<Time>();
            int frameRate = time.TARGET_FRAME_RATE;
            
            agent.Run(Update);
            return;


            void Update()
            {
                if (agent.Has<Kinematic>())
                    return;


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
                    agent.View.transform.DOKill();
                    agent.View.transform.DOLookAt(body.LookPoint, agent.Config.LookSmoothValue);
                }
            }
        }
        
    }
}
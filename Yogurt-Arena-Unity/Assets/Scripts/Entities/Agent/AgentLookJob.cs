using DG.Tweening;

namespace Yogurt.Arena
{
    public struct AgentLookJob : IUpdateJob
    {
        public void Update()
        {
            foreach (AgentAspect agent in Query.Of<AgentAspect>())
            {
                // if (agent.State.Velocity.magnitude < 0.01f) continue;

                BodyState body = agent.Body;
                
                if (agent.BattleState.Target.Exist)
                {
                    BodyState targetBody = agent.BattleState.Target.Get<BodyState>();
                    body.LookTarget = targetBody.Position;
                }
                else
                {
                    body.LookTarget = body.Position + body.Velocity.WithY(0);
                }
                body.LookTarget.y = body.Position.y;
                
                agent.View.transform.DOLookAt(agent.Body.LookTarget, 0.3f);
            }
        }
    }
}
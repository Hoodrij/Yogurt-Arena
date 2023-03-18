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

                if (agent.BattleState.Target.Exist)
                {
                    BodyState targetBody = agent.BattleState.Target.Get<BodyState>();
                    agent.Body.LookTarget = targetBody.Position;
                }
                else
                {
                    BodyState body = agent.Body;
                    body.LookTarget = body.Position + body.Velocity.WithY(0);
                }
                
                agent.View.transform.DOLookAt(agent.Body.LookTarget, 0.3f);
            }
        }
    }
}
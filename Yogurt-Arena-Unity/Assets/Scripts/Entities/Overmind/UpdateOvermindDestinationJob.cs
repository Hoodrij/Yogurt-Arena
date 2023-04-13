using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateOvermindDestinationJob : IUpdateJob
    {
        public void Update()
        {
            int agentIndex = 0;
            
            foreach (AgentAspect agent in Query.Of<AgentAspect>().Without<PlayerTag>())
            {
                if (agent.BattleState.Target.Exist())
                {
                    agent.Body.Destination = agent.BattleState.Target.Get<BodyState>().Position;
                }
                else
                {
                    float moveRadius = 0.1f;
                    float time = Time.fixedTime + agentIndex;
                    agent.Body.Destination += new Vector3(moveRadius * Mathf.Sin(time), 0, moveRadius * Mathf.Cos(time));
                    agentIndex++;
                }
            }
        }
    }
}
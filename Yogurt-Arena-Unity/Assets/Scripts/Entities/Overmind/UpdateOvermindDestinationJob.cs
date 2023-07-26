using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateOvermindDestinationJob
    {
        public void Run(OvermindAspect overmind)
        {
            overmind.Run(Update);
            
            
            void Update()
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
                        float time = UnityEngine.Time.fixedTime + agentIndex;
                        agent.Body.Destination += new Vector3(moveRadius * Mathf.Sin(time), 0, moveRadius * Mathf.Cos(time));
                        agentIndex++;
                    }
                }
            }
        }
    }
}
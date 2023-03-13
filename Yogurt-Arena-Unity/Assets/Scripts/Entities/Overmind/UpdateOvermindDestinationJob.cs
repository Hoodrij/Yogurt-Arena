using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateOvermindDestinationJob : IUpdateJob
    {
        public void Update()
        {
            AgentAspect player = Query.Of<AgentAspect>().With<PlayerTag>().Single();

            int agentIndex = 0;
            
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>().Without<PlayerTag>())
            {
                // agentAspect.State.Destination = player.State.Position;
                
                float moveRadius = 0.1f;
                float time = Time.realtimeSinceStartup + agentIndex;
                agentAspect.State.Destination += new Vector3(moveRadius * Mathf.Sin(time), 0, moveRadius * Mathf.Cos(time));
                agentIndex++;
            }
        }
    }
}
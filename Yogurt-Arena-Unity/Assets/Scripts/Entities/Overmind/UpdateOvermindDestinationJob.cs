﻿namespace Yogurt.Arena
{
    public struct UpdateOvermindDestinationJob : IUpdateJob
    {
        public void Update()
        {
            AgentAspect player = Query.Of<AgentAspect>().With<PlayerTag>().Single();

            int agentIndex = 0;
            
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>().Without<PlayerTag>())
            {
                agentAspect.Body.Destination = player.Body.Position;
                
                // float moveRadius = 0.1f;
                // float time = Time.fixedTime + agentIndex;
                // agentAspect.Body.Destination += new Vector3(moveRadius * Mathf.Sin(time), 0, moveRadius * Mathf.Cos(time));
                // agentIndex++;
            }
        }
    }
}
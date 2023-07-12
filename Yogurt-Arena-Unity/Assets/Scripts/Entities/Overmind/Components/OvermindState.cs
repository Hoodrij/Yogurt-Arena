using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class OvermindState : IComponent
    {
        public List<AgentAspect> Agents = new List<AgentAspect>();

        public async void AddAgent(AgentAspect agent)
        {
            Agents.Add(agent);
            await agent.Entity.WaitForDead();
            Agents.Remove(agent);
        } 
    }
}
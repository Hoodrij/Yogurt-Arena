using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class OvermindState : IComponent
    {
        public List<AgentAspect> Agents = new List<AgentAspect>();

        public bool HasEnoughAgents()
        {
            int minimumAgents = Query.Single<Data>().Overmind.MinimumAgents;
            return Agents.Count > minimumAgents;
        }
        
        public async void KeepAgent(AgentAspect agent)
        {
            Agents.Add(agent);
            await agent.Entity.WaitForDead();
            Agents.Remove(agent);
        } 
    }
}
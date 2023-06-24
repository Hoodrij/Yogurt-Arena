using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class OvermindState : IComponent
    {
        public List<AgentAspect> Agents = new List<AgentAspect>();

        public async UniTaskVoid AddAgent(AgentAspect agent)
        {
            Agents.Add(agent);
            await agent.Entity.WaitForDead();
            Agents.Remove(agent);
        } 
    }
}
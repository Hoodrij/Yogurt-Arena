using System;

namespace Yogurt.Arena
{
    [Flags]
    public enum Team
    {
        None = 0,
        Green = 1,
        Red = 2,
    }
    
    public class AgentId : IComponent
    {
        public Team Team;
    }
}
﻿namespace Yogurt.Arena;

[Flags]
public enum TeamType
{
    None = 0,
    Green = 1,
    Red = 2,
}
    
public class AgentId : IComponent
{
    public TeamType teamType;
}
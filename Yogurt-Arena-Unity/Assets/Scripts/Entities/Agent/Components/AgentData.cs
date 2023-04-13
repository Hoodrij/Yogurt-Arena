using System;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class AgentData : IComponent
    {
        public PooledAsset<AgentView> Asset;
        public float MoveSpeed;
        public float MoveSmoothValue;
        public float FindTargetDistance;
        public float LookSmoothValue;
        public int Health;
    }
}
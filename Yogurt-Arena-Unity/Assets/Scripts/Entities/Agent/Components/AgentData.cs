using System;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class AgentData : IComponent
    {
        public PooledAsset<AgentView> Asset;
        public float MoveSpeed;
        public float SlowDistance;
        public float MoveSmoothValue;
        public float FindTargetDistance;
        public float LookSmoothValue;
        public int MaxHealth;
        public int Health;
    }
}
using System;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class AgentConfig : IComponent
    {
        public PooledAsset<AgentView> Asset;
        public float MoveSpeed;
        public float SlowDistance;
        public float MoveSmoothValue;
        public float LookSmoothValue;
        public int MaxHealth;
        public int Health;
    }
}
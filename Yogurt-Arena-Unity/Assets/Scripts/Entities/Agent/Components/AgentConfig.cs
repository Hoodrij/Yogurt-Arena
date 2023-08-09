using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class AgentConfig : ScriptableObject, IComponent, IConfig
    {
        public PooledAsset<AgentView> Asset;
        public AgentType Type;
        public TeamType Team;
        public ItemType Weapon;
        public float MoveSpeed;
        public float SlowDistance;
        public float MoveSmoothValue;
        public float LookSmoothValue;
        public int MaxHealth;
        public int Health;

        public void AppendTo(Entity entity)
        {
            entity.Add(this);
        }
    }
}
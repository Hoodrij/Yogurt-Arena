using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class AgentConfig : ScriptableObject, IComponent, IEntityConfig
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

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
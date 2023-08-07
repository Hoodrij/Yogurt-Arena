using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class HealingPotionConfig : ScriptableObject, IComponent, IConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new HealingPotionFactoryJob(),
            UseJob = new UseHealingPotionJob()
        };
        public int Amount;
        
        public void AppendTo(Entity entity)
        {
            entity.Add(this)
                .Add(Item);
        }
    }
}
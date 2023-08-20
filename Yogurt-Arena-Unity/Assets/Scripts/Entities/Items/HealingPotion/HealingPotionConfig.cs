using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class HealingPotionConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new HealingPotionFactoryJob(),
            UseJob = new UseHealingPotionJob()
        };
        public int Amount;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
        }
    }
}
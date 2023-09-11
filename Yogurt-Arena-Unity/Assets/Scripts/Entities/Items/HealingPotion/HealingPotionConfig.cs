using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    public class HealingPotionConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public ItemConfig Item = new()
        {
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
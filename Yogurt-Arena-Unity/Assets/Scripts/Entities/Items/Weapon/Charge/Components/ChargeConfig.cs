using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    public class ChargeConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseChargeJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return TargetDetection;
        }
    }
}
﻿using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class RifleConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public ItemConfig Item = new()
        {
            FactoryJob = new CommonWeaponFactoryJob(),
            UseJob = new UseRifleJob(),
        };
        public WeaponConfig Weapon;
        public ItemLifetimeConfig Lifetime;
        public WeaponScatteringConfig Scattering;
        public TargetDetectionConfig TargetDetection;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return Item;
            yield return Weapon;
            yield return Lifetime;
            yield return Scattering;
            yield return TargetDetection;
        }
    }
}
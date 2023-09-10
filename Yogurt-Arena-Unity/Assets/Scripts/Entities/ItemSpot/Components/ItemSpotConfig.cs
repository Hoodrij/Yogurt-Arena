﻿using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public int Radius;
        public LayerMask Mask;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
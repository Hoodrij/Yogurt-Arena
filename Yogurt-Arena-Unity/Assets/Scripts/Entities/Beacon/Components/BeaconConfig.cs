using System;
using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class BeaconConfig : ScriptableObject, IComponent, IConfigSO
    {
        public Asset<BeaconView> Asset;
        public float SmoothValue;
        public float Elasticity;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
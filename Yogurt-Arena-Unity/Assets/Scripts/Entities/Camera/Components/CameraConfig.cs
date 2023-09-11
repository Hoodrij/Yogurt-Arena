using System;
using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class CameraConfig : ScriptableObject, IComponent, IConfigSO
    {
        public Asset<CameraView> Asset;
        public float SmoothValue;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
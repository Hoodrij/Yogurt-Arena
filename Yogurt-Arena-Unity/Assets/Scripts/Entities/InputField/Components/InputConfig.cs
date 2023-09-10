using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public class InputConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public Asset<InputFieldView> Asset;
        public float AccumulativeValue;
        public float DeAccumulativeValue;
        public float Sensitivity;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
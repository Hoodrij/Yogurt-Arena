using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public class InputConfig : ScriptableObject, IComponent, IConfigSO
    {
        public Asset<InputFieldView> Asset;
        public LayerMask LayerMask = LayerMask.NameToLayer("Ground");
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
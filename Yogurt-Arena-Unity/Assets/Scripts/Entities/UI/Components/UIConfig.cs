using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public class UIConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public Asset<UIView> UI;
        public Asset<WorldUIView> WorldUI;
        public PooledAsset<HealthWidget> WorldHealthWidget;
        
        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}
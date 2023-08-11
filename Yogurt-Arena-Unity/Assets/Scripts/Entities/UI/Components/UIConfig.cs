using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class UIConfig : IComponent
    {
        public Asset<UIView> UI;
        public Asset<WorldUIView> WorldUI;
        public PooledAsset<HealthWidget> WorldHealthWidget;
    }
}
using UnityEngine.UIElements;

namespace Yogurt.Arena
{
    public class UIElementsWeaponLifetimeWidget : WeaponLifetimeWidget
    {
        private VisualElement fill;
        private float progress;

        public void Bind(VisualElement root)
        {
            fill = root.Q<VisualElement>("weapon-fill");
            fill.style.width = Length.Percent(progress * 100f);
        }

        public void Unbind() => fill = null;

        public override void SetProgress(float value)
        {
            progress = Mathf.Clamp01(value);
            if (fill != null)
                fill.style.width = Length.Percent(progress * 100f);
        }
    }
}

using UnityEngine.UIElements;

namespace Yogurt.Arena
{
    public class UIElementsHealthWidget : HealthWidget
    {
        private VisualElement fill;
        private IVisualElementScheduledItem resetFlash;
        private float displayedPercentage;
        private bool hasValue;

        public void Bind(VisualElement root)
        {
            Unbind();
            fill = root.Q<VisualElement>("health-fill");
            fill.style.width = Length.Percent(hasValue ? displayedPercentage * 100f : 100f);
        }

        public void Unbind()
        {
            resetFlash?.Pause();
            resetFlash = null;
            fill = null;
        }

        public override UniTaskVoid SetHealth(float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            bool damaged = displayedPercentage > percentage;
            bool healed = displayedPercentage > 0 && displayedPercentage < percentage;
            displayedPercentage = percentage;
            hasValue = true;

            if (fill == null)
                return default;

            fill.style.width = Length.Percent(percentage * 100f);
            fill.EnableInClassList("health-fill--damage", damaged);
            fill.EnableInClassList("health-fill--heal", healed);
            resetFlash?.Pause();
            resetFlash = fill.schedule.Execute(() =>
            {
                fill.RemoveFromClassList("health-fill--damage");
                fill.RemoveFromClassList("health-fill--heal");
            }).StartingIn(100);
            return default;
        }
    }
}

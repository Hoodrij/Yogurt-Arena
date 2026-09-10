using UnityEngine.UIElements;

namespace Yogurt.Arena
{
    public class UIElementsHealthWidget : HealthWidget
    {
        private VisualElement fill;
        private IVisualElementScheduledItem resetFlash;
        private float currentPercentage;
        private bool hasValue;

        public void Bind(VisualElement root)
        {
            Unbind();
            fill = root.Q<VisualElement>("health-fill");
            fill.style.width = Length.Percent(hasValue ? currentPercentage * 100f : 100f);
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
            bool damaged = currentPercentage > percentage;
            bool healed = currentPercentage > 0 && currentPercentage < percentage;
            currentPercentage = percentage;
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

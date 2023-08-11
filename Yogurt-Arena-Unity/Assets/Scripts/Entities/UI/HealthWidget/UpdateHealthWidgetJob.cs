namespace Yogurt.Arena
{
    public struct UpdateHealthWidgetJob
    {
        public void Run(Health health)
        {
            if (health.HealthWidget == null) return;
            
            float percentage = (float) health.Value / health.MaxHealth;

            HealthWidget healthWidget = health.HealthWidget;
            healthWidget.SetHealth(percentage);
        }
    }
}
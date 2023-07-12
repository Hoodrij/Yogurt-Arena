namespace Yogurt.Arena
{
    public struct UpdatePlayerHealthUIJob : ChangeHealthJob.IHealthChangedJob
    {
        public void Run(Health health)
        {
            float percentage = (float) health.Value / health.MaxHealth;

            PlayerHealthWidget playerHealthWidget = Query.Single<UIView>().PlayerHealthWidget;
            playerHealthWidget.SetHealth(percentage);
        }
    }
}
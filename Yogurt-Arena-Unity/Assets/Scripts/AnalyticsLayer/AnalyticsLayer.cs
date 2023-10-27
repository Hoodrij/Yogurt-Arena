namespace Yogurt.Arena.Analytics
{
    public struct AnalyticsLayer
    {
        public void Run()
        {
            new WeaponPickedUpAnalyticsJob().Run();
        }
    }
}